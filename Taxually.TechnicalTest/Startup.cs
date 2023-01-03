using Microsoft.OpenApi.Models;
using Taxually.TechnicalTest.Infrastructure.Extensions;
using Taxually.TechnicalTest.Infrastructure.Mappers;
using Taxually.TechnicalTest.Service;

namespace Taxually.TechnicalTest
{
    public class Startup
    {
        //private readonly ILogger _logger;
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
            //_logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSingleton<IVATRegistrationMapper, VATRegistrationMapper>();
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<IAutoMapperConfiguration, AutoMapperConfiguration>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI",
                    Description = "A simple example to Implement Swagger UI",
                });
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
               // _logger.LogInformation("In Development Environment");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.MapControllerRoute(  
            //name: "default",
            //pattern: "{controller=VatRegistration}/{action=Get}");  Routing can be configured here or in other class.

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });


            //app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
