using Microsoft.AspNetCore.Mvc;
using Talabat.API.Helper.MappingProfile;
using Talabat.API.MiddleWare;
using Talabat.CoreEntities.Repositotry;
using Talabat.Repository;

namespace Talabat.API.Extension
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(Mapping));
            services.AddTransient<ExceptionMW>();
            // this allow u to configure and customize BadRequest with ur own response
            services.Configure<ApiBehaviorOptions>(opt =>
             {
                 opt.InvalidModelStateResponseFactory = (actionContex) =>
                 {
                     var errors = actionContex.ModelState.Where(a => a.Value.Errors.Count() > 0)
                     .SelectMany(a => a.Value.Errors)
                     .Select(a => a.ErrorMessage)
                     .ToList();

                     return new BadRequestObjectResult(errors);
                 };
             });
        }
        public static void AddSwaager(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
