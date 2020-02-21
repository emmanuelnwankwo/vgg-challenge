using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.BusinessLayer.Interface;
using WebApp.Core.BusinessLayer.Repository;
using WebApp.Core.EntityClass;
using WebApp.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using WebApp.Core.Dtos;
using WebApp.Core.Utility;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Api
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
            services.AddCors();
            services.AddControllers();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            // configure jwt authentication
            byte[] key;
            if (env == "Development")
            {
                key = Encoding.ASCII.GetBytes(appSettings.Secret);
            }
            else
            {
                string secret =Environment.GetEnvironmentVariable("Secret");
                key = Encoding.ASCII.GetBytes(secret);
            }
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });
            if (env == "Development")
            {
                services.AddEntityFrameworkSqlServer().AddDbContext<ApiDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("WebApiDB"));
                });
            }
            else
            {
                services.AddEntityFrameworkNpgsql().AddDbContext<ApiDbContext>(options =>
                {
                    options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"));
                });
            }
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();

            services.AddScoped<UserEntity>();
            services.AddScoped<ProjectEntity>();
            services.AddScoped<ActionEntity>();
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("WebApi",
                    new OpenApiInfo()
                    {
                        Title = "Web Api",
                        Version = "v1"
                    });
                setupAction.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "Please enter into field the word 'Bearer' following by space and Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        new List<string>()
                      }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger(c =>
            {
                //Change the path of the end point , should also update UI middle ware for this change                
                c.RouteTemplate = "{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/WebApi/swagger.json", "");
            });
            app.UseRouting();
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 401)
            //    {
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync("eeeeeeeee");
            //    }
            //});
            //app.UseStatusCodePages(async context =>
            //{
            //    if (context.HttpContext.Response.StatusCode.Equals(401))
            //    {
            //        //ErrorResponse errorResponse = new ErrorResponse
            //        //{
            //        //    Status = 401,
            //        //    Message = "Access denied, invalid token"
            //        //};
            //        await context.HttpContext.Response.WriteAsync("Unauthorized request");
            //        //return errorResponse;
            //    }
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
