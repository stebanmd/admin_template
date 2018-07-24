using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            IoC.IoCConfiguration.Configure(services);
            services.AddSingleton(Configuration);            
            Mappings.AutoMapperConfiguration.Initialize();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(op =>
            {
                op.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                op.Filters.Add(new RequireHttpsAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(op =>
                {
                    op.ExpireTimeSpan = TimeSpan.FromDays(10);
                    op.AccessDeniedPath = "/Home/AccessDenied";
                    op.LoginPath = "/Account/Login";
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHsts(op => op.MaxAge(days: 3650).IncludeSubdomains());
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseXXssProtection(op => op.EnabledWithBlockMode());
            app.UseXContentTypeOptions();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}