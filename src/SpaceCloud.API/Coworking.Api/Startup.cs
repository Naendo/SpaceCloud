using System;
using System.Net.Http.Headers;
using AspNetCore.Uploader;
using BackgroundQueue;
using Coworking.Api;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Options;
using Coworking.Application.Services;
using Coworking.Infrastructure;
using Coworking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Coworking
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtOptions = new JwtOptions();
            Configuration.GetSection(JwtOptions.Position).Bind(jwtOptions);
            services.Configure<JwtOptions>(Configuration.GetSection(JwtOptions.Position));

            services.Configure<MailOptions>(Configuration.GetSection(MailOptions.POSITION));
            services.Configure<BlobOptions>(Configuration.GetSection(BlobOptions.Position));
            services.Configure<ApplicationOptions>(x => x.InvoiceNr = 10000);
            services.Configure<CurrencyOptions>(Configuration.GetSection(CurrencyOptions.POSITION));
            services.Configure<FileOptions>(Configuration.GetSection(FileOptions.POSITION));
            services.Configure<TeamupOptions>(Configuration.GetSection(TeamupOptions.POSITION));

            services.AddLogging(builder => builder.AddConfiguration(Configuration.GetSection("Logging"))
                .SetMinimumLevel(LogLevel.Error));

            services.AddCache(Environment.GetEnvironmentVariable("SPACECLOUD_REDIS") ?? "localhost");
            services.AddMailService();


            services.AddScoped<CurrencyService>();
            services.AddScoped<FileManager>();

            services.AddAvailabilityService();
            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder
                        .SetIsOriginAllowed(host => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                );
            });

            if (Env.EnvironmentName != "Integration")
                services.AddDbContext<WorkingContext>(options =>
                {
                    if (Env.IsDevelopment())
                        options.UseNpgsql(Configuration.GetConnectionString("localdb"));
                    if (Env.IsProduction())
                        options.UseNpgsql(Configuration.GetConnectionString("workCT"));
                });


            services.AddSwagger();

            services.AddDashboard();

            services.AddAuthenticationAndAuthoriation(jwtOptions);

            services.AddBackgroundTaskQueue();


            services.AddRepositories();


            services.AddApplication(Configuration.GetSection(AuthenticationOptions.POSITION)
                .Get<AuthenticationOptions>());


            services.AddTransient<TeamupService>();

            services.AddHttpClient("teamup", options =>
            {
                options.BaseAddress = new Uri("https://api.teamup.com/ks4w6hoxzddh1bbjw2/");
                options.DefaultRequestHeaders.Add("Teamup-Token", Configuration["TeamUpOptions:ApiKey"]);
            });


            services.AddBlobService();

            services.AddHealthChecks();

            services.AddFilters();


            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coworking Api V1"); });


            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<DashboardHub>("/dashboard");
            });
        }
    }
}