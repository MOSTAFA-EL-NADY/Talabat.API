using Microsoft.EntityFrameworkCore;
using Talabat.API.Extension;
using Talabat.API.MiddleWare;
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

            builder.Services.AddDbContext<StoreContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            builder.Services.AddApplicationService();

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
            // map  404 error to this endpoint
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.AddSwaager();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}