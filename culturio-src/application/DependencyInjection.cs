using Culturio.Application.Companies;
using Culturio.Application.CultureObjectCompanies;
using Culturio.Application.CultureObjects;
using Culturio.Application.CultureObjects.Models;
using Culturio.Application.CultureObjects.Validators;
using Culturio.Application.Persistence;
using Culturio.Application.Users;
using Culturio.Application.Visits;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICultureObjectCompanyService, CultureObjectCompanyService>();
            services.AddScoped<ICultureObjectService, CultureObjectService>();
            services.AddScoped<IVisitService, VisitService>();

            services.AddScoped<IValidator<CreateCultureObjectDto>, CreateCultureObjectValidator>();
            services.AddScoped<IValidator<GetCultureObjectsDto>, GetCultureObjectsDtoValidator>();
            services.AddScoped<IValidator<CreateCultureObjectDto>, CreateCultureObjectValidator>();
            services.AddScoped<IValidator<UpdateCultureObjectDto>, UpdateCultureObjectValidator>();

            services.AddDbContext<CulturioDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CulturioDb"));
            });

            return services;
        }
    }
}
