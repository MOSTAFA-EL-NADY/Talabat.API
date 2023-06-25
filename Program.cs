using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            catch(Exception ex)
            {
                logger.LogError(ex,ex.Message);
            }

            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}