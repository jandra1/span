using System;
using Culturio.Application.Collections;
using Culturio.Application.Persistence;
using Culturio.Application.Users;
using Culturio.Application.Users.Models;
using Culturio.Application.Visits.Models;
using Culturio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Culturio.Application.Visits
{
    public class VisitService : IVisitService
    {
        private readonly CulturioDbContext _context;
        private readonly ILogger<VisitService> _logger;

        public VisitService(CulturioDbContext context, ILogger<VisitService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> CreateVisit(CreateVisitDto visit, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(visit, nameof(visit));

            Visit visitToCreate = new()
            {
                UserId = visit.UserId,
                CultureObjectId = visit.CultureObjectId,
                TimeOfVisit = visit.TimeOfVisit //DateTime.Now.Date
            };

            _context.Visits.Add(visitToCreate);
            await _context.SaveChangesAsync(token);

            return visitToCreate.Id;
        }

        public async Task<IPagedList<VisitDto>> GetVisits(GetVisitDto getVisitDto, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(getVisitDto, nameof(getVisitDto));

            IQueryable<Visit> visitQuery = _context.Visits.AsNoTracking();

            visitQuery = ApplyFilters(getVisitDto, visitQuery);

            int totalCount = await visitQuery.CountAsync(token);
            visitQuery = ApplySortingAndPaging(getVisitDto, visitQuery);

            var visits = await visitQuery.Select(visit => new VisitDto
            {
                Id = visit.Id,
                UserId = visit.UserId,
                CultureObjectId = visit.CultureObjectId,
                TimeOfVisit = visit.TimeOfVisit
            }).ToListAsync(cancellationToken: token);

            return new PagedList<VisitDto>(visits, getVisitDto.Page ?? 1, getVisitDto.PageSize ?? int.MaxValue, totalCount);
        }

        private IQueryable<Visit> ApplySortingAndPaging(GetVisitDto getVisitDto, IQueryable<Visit> visitQuery)
        {
            visitQuery = getVisitDto.SortOrder switch
            {
                "asc" => visitQuery.OrderBy(x => x.TimeOfVisit),
                "desc" => visitQuery.OrderByDescending(x => x.TimeOfVisit),
                _ => visitQuery.OrderByDescending(x => x.TimeOfVisit)
            };

            if (getVisitDto.Page.HasValue && getVisitDto.PageSize.HasValue)
            {
                visitQuery = visitQuery.Skip((getVisitDto.Page.Value - 1) * getVisitDto.PageSize.Value)
                    .Take(getVisitDto.PageSize.Value);
            }

            return visitQuery;
        }

        private static IQueryable<Visit> ApplyFilters(GetVisitDto getVisitDto, IQueryable<Visit> visitQuery)
        {
            if (!string.IsNullOrWhiteSpace(getVisitDto.SearchValue))
            {
                visitQuery = visitQuery.Where(visit => visit.UserId.Equals(getVisitDto.SearchValue));
            }
            if (getVisitDto.UserId.HasValue)
            {
                visitQuery = visitQuery.Where(user => user.UserId == getVisitDto.UserId.Value);
            }
            return visitQuery;
        }

        public async Task<IPagedList<VisitDto>> GetByUserId(GetVisitDto getVisitDto, int id, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(getVisitDto, nameof(getVisitDto));

            IQueryable<Visit> visitQuery = _context.Visits.AsNoTracking();

            visitQuery = ApplyFilters(getVisitDto, visitQuery);

            visitQuery = ApplySortingAndPaging(getVisitDto, visitQuery);

            var visits = await visitQuery.Where(visit => visit.UserId == id).Select(visit => new VisitDto
            {
                Id = visit.Id,
                UserId = visit.UserId,
                CultureObjectId = visit.CultureObjectId,
                TimeOfVisit = visit.TimeOfVisit
            }).ToListAsync(cancellationToken: token);

            int totalCount = await visitQuery.CountAsync(token);
            totalCount = visits.Count;

            return new PagedList<VisitDto>(visits, getVisitDto.Page ?? 1, getVisitDto.PageSize ?? int.MaxValue, totalCount);
        }
    }
}

