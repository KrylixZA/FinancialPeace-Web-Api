using System;
using System.IO;
using System.Reflection;
using FinancialPeace.Web.Api.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Shared.WebApi.Core.Builders;
using Shared.WebApi.Core.Errors;
using Shared.WebApi.Core.Extensions;
using Shared.WebApi.Core.Security;

#pragma warning disable 1591

namespace FinancialPeace.Web.Api
{
    public class Startup
    {
        private const string ApiTitle = "Financial Peace";
        private const int ApiVersion = 1;
        private const string ApiDescription = "A RESTful API that exposes functionality to manage your way to financial peace.";
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Setup the API
            services.AddControllers();
            services.AddTokenAuthentication(Configuration);

            // Setup the Swagger docs.
            var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);

            new SwaggerServicesBuilder()
                .WithApiTitle(ApiTitle)
                .WithApiVersion(ApiVersion)
                .WithApiDescription(ApiDescription)
                .WithXmlComments(apiXmlPath)
                .WithCoreXmlDocs(true)
                .WithJwtAuthentication(true)
                .BuildSwaggerServices(services);
            
            // Register implementations
            services.TryAddTransient<IErrorMessageSelector, ErrorMessageSelector>();
            services.TryAddTransient<IJwtService, JwtService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwaggerDocs(ApiVersion, ApiTitle);
            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();  
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}