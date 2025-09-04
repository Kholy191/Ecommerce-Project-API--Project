
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Presistence;
using Presistence.Identity;
using Presistence.UnitOfWork;
using ServiceAbstraction;
using Services;
using Services.AutoMapperProfile;
using Web_Api_Application.CustomMiddlewares;

namespace Web_Api_Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Service Configuration
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddPresistenceConfig(builder.Configuration); // Custom extension method to add persistence layer configurations
            builder.Services.AddServiceConfig();// Custom extension method to add service layer configurations

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityStore>(); // Configuring Identity with Entity Framework Store

            builder.Services.AddSwaggerGen();

            #region Invalid Model State Response Factory Configuration

            builder.Services.Configure<ApiBehaviorOptions>(ApiBehaviorOptions => 
            {
                ApiBehaviorOptions.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new ErrorModels.ValidationError
                        {
                            Key = e.Key,
                            Errors = e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                        }).ToArray();
                    var Error = new ErrorModels.ValidationErrorToReturn
                    {
                        Errors = errors,
                    };
                    return new BadRequestObjectResult(Error);
                };
            });

            #endregion

            #endregion

            var app = builder.Build();

            #region Exception Handler Middleware Configuration
            app.UseMiddleware<CustomExceptionMiddleware>();
            #endregion

            #region Data Seeding Configuration
            using (var Scope = app.Services.CreateScope())
            {
                var dataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                await dataSeeding.SeedDataAsync();
                await dataSeeding.SeedIdentityDataAsync();
            }
            #endregion

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
