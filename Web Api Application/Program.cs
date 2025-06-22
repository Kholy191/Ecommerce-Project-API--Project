
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.UnitOfWork;
using ServiceAbstraction;
using Services;
using Services.AutoMapperProfile;

namespace Web_Api_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<Presistence.Data.ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSwaggerGen();
            
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            var app = builder.Build();

            using(var Scope = app.Services.CreateScope())
            {
                var dataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                dataSeeding.SeedDataAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
