using System;
using Culturio.API.Models;
using Culturio.Application.Visits;
using Culturio.Application.Visits.Models;
using Culturio.Application.Visits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using IdentityModel;
using Culturio.Application.Users.Models;
using Culturio.Application.Users;

namespace Culturio.API.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class VisitController : ControllerBase
        {

            private readonly ILogger<VisitController> _logger;
            private readonly IVisitService _visitService;
            private readonly IUserService _userService;

            public VisitController(ILogger<VisitController> logger, IVisitService visitService, IUserService userService)
            {
                _logger = logger;
                _visitService = visitService;
                _userService = userService;
            }

            [HttpGet(Name = nameof(GetVisits))]
            [RequiredScope("tasks.read")]
            public async Task<GetVisitsResponseModel> GetVisits([FromQuery] GetVisitDto model, CancellationToken token)
            {
                _logger.LogInformation("Fetching visits");

                var visits = await _visitService.GetVisits(model, token);

                return new GetVisitsResponseModel(visits);
            }

            [HttpGet("visitsOfUser", Name = nameof(GetVisit))]
            public async Task<GetVisitsResponseModel> GetVisit([FromQuery] GetVisitDto model, CancellationToken token)
            {
                _logger.LogInformation("Fetching visit");

                var sub = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject).Value;

                UserAuthDto userAuthDto = await _userService.GetUserInfo(sub, token);

                var visits = await _visitService.GetByUserId(model, userAuthDto.Id, token);

                return new GetVisitsResponseModel(visits);
        }

            [HttpPost(Name = nameof(CreateVisit))]
            public async Task<IActionResult> CreateVisit([FromBody] CreateVisitDto model, CancellationToken token)
            {
                _logger.LogInformation("Creating a new visit");

                int visitId = await _visitService.CreateVisit(model, token);

                return CreatedAtAction(nameof(CreateVisit), new { Id = visitId });
            }
        }
 }


