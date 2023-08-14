using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.API.Helper.MappingProfile;
using Talabat.API.MiddleWare;
using Talabat.CoreEntities.Repositotry;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeeding;

namespace Talabat.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(Mapping));
            builder.Services.AddTransient<ExceptionMW>();
            // this allow u to configure and customize BadRequest with ur own response
            builder.Services.Configure<ApiBehaviorOptions>(opt =>
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

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

            try
            {

                var context = services.GetRequiredService<StoreContext>();
                context.Database.Migrate();
                StoreSeed.SeedData(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }

            // Configure the HTTP request pipeline.

            // this mw for customize server error response 
            app.UseMiddleware<ExceptionMW>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}