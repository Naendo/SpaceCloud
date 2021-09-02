using System.Text;
using System.Threading.Tasks;
using Coworking.Api.Filters;
using Coworking.Application.Authentication;
using Coworking.Application.Authentication.UserManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Coworking.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDashboard(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<ServerHubContext>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Coworking Api", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthoriation(this IServiceCollection services,
            JwtOptions jwtOptions)
        {
            //Authentication
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        RequireExpirationTime = false
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var path = context.HttpContext.Request.Path;
                            if (!path.StartsWithSegments("/dashboard")) return Task.CompletedTask;

                            string token = context.Request.Query["access_token"];

                            if (context.Request.Headers.TryGetValue("Authorization", out var value))
                                token = value.ToString()["Bearer ".Length..];


                            // If the request is for our hub...

                            if (!string.IsNullOrEmpty(token) &&
                                path.StartsWithSegments("/dashboard"))
                                // Read the token out of the query string
                                context.Token = token;

                            return Task.CompletedTask;
                        }
                    };
                });


            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityPolicies.ADMIN_POLICY,
                    policy => policy.RequireRole("Administrator", "Owner"));
                options.AddPolicy(IdentityPolicies.OWNER_POLICY,
                    policy => policy.RequireRole("Owner"));
                options.AddPolicy(IdentityPolicies.WORKER_POLICY,
                    policy => policy.RequireRole("Worker", "Owner", "Administrator"));
                options.AddPolicy(IdentityPolicies.READONLY,
                    policy => { policy.Requirements.Add(new ScheduleRequirement()); });
            });

            services.AddScoped<TokenService>();
            services.AddHttpContextAccessor();
            services.AddScoped<UserAccessor>();
            services.AddScoped<IAuthorizationHandler, ScheduleRequirementHandler>();
            return services;
        }

        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            services.AddScoped<CustomExceptionFilter>();
            services.AddScoped<CustomActionFilter>();
            services.AddMvc(x =>
            {
                x.EnableEndpointRouting = false;
                x.Filters.Add(typeof(CustomExceptionFilter));
                x.Filters.Add(typeof(CustomActionFilter));
                x.Filters.Add(typeof(TenantFilter));
            });
            return services;
        }
    }
}