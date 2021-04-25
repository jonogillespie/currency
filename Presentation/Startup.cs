using Application;
using Application.Interfaces.External;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Middleware.ErrorHandling;

namespace Presentation
{
    public class Startup
    {
        private const string CorsPolicyName = "CORS";

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add(new ProducesAttribute("application/json"));
            });
            services.AddInfrastructure();
            services.AddApplication();
            
            services.AddSwaggerGen();
            
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder =>
                    {
                        builder.WithOrigins("*");
                    });
            });

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            app.UseCors(CorsPolicyName);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currencies API");
            });

            app.Use((context, next) =>
            {
                var requestIdService =
                    (IRequestIdService) context.RequestServices.GetService(typeof(IRequestIdService));
                context.Response.Headers.Add("X-Request-ID", requestIdService?.GetRequestId());
                return next.Invoke();
            });
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });
        }
    }
}