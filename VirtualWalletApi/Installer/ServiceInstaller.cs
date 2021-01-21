using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VirtualWalletApi.Data.Entities;

namespace VirtualWalletApi.Installer
{
    public class ServiceInstaller
    {
        public static void InstallDatabase(IServiceCollection services)
        {
            services.AddDbContext<VirtualWalletDbContext>(opt => opt.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION")));
            services.AddScoped<DbContext, VirtualWalletDbContext>();
        }

        public static void InstallJwtAuthorization(IServiceCollection services)
        {
            var tokenConfig = new TokenConfiguration();
            services.AddAuthentication(x =>

            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig.Secret)),
                    ValidIssuer = tokenConfig.Issuer,
                    ValidAudience = tokenConfig.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromMinutes(tokenConfig.AccessExpiration)
                };
            });
        }

        public static void InstallSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = Environment.GetEnvironmentVariable("APP_NAME"),
                    Description = Environment.GetEnvironmentVariable("APP_DESCRIPTION")
                });

                //Allows swagger to authorize endpoints
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
                                },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            },
                            new string[] {}
                    }
                });
               // Configure Swagger to use the xml documentation file
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }
        public class TokenConfiguration
        {
            public string Issuer { set; get; } = Environment.GetEnvironmentVariable("TOKEN_ISSUER");
            public string Audience { set; get; } = Environment.GetEnvironmentVariable("TOKEN_AUDIENCE");
            public int AccessExpiration { set; get; } = Convert.ToInt32(Environment.GetEnvironmentVariable("TOKEN_ACCESS_EXP"));
            public string Secret { set; get; } = Environment.GetEnvironmentVariable("TOKEN_SECRET");
        }
    }
}
