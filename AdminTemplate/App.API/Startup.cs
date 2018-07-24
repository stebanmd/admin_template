using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace App.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            App.IoC.IoCConfiguration.Configure(services);

            services.AddSingleton(Configuration);
            services.AddScoped<Validators.AppValidator>();

            Mappings.AutoMapperConfiguration.Initialize();

            var pathToDoc = Configuration["Swagger:Path"];

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "App API", Version = "v1" });
                c.IncludeXmlComments(pathToDoc);
                c.DescribeAllEnumsAsStrings();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                    builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors("AllowAllOrigin");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });

        }
    }
}