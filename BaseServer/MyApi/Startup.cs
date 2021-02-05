using System;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebFramework.Configuration;
using WebFramework.Middlewares;
using WebFramework.Swagger;
using WebFramework.Session;
using Data.Repositories;
using Services.BS.Contracts;
using Services.BS.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Common.Utilities;

namespace Refah
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                                  {
                                      builder.WithOrigins("https://localhost:44300",
                                          "https://localhost:44339",
                                          "https://localhost:44364",
                                          "https://digipardis.com/",
                                          "https://adminpanel.digipardis.com")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials();
                                  });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBSService<>), typeof(BSService<>));

            services.AddSession(z =>
            {
                z.Cookie.IsEssential = true;
                z.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(_siteSetting.IdentitySettings);
            services.AddMinimalMvc();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddSwagger();

            services.AddSwaggerGen();

            services.AddSessionService();

            return services.BuildAutofacServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(MyAllowSpecificOrigins);

            app.IntializeDatabase();

            app.UseCustomExceptionHandler();
            app.UseStaticFiles();
            app.UseHsts(env);

            app.UseHttpsRedirection();

            app.UseSwaggerAndUI();

            app.UseAuthentication();

            app.UseSessionService();

            app.UseMvc((routes) =>
            {
                routes.MapRoute("default", "{controller=Moradi}/{action=Index}/{param?}/{paramAction?}");
            });

        }
    }
}
