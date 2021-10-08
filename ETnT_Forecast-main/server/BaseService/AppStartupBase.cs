using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using AutoWrapper;
using BaseService.Extensions;
using DataAccess;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BaseService
{
    public abstract class AppStartupBase
    {
        protected AppStartupBase(IWebHostEnvironment env, IConfiguration configuration,
            string[] settingsFiles = default)
        {
            var builder = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddJsonFile(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"sharedsettings.{env.EnvironmentName}.json"),
                    false, true);

            if (settingsFiles != null && settingsFiles.Any())
                foreach (var file in settingsFiles.ToList())
                    builder.AddJsonFile(file);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }


        protected void ConfigureApplicationServices(IServiceCollection services, OpenApiInfo serviceInfo)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSingleton(Configuration);
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin",
                    options =>
                        options
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials()
                );
            });

            services.AddLogging();
            services.AddAutoMapper(AppDomain.CurrentDomain.Load("Service"));
            // services.AddAuthentication(x =>
            // {
            //     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey =
            //             new SymmetricSecurityKey(
            //                 Encoding.ASCII.GetBytes(Configuration.GetSection("Token").Value)),
            //         ValidateIssuer = false,
            //         ValidateAudience = false
            //     };
            // });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(serviceInfo.Version, serviceInfo);
                // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     Name = "Authorization",
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer",
                //     BearerFormat = "JWT",
                //     In = ParameterLocation.Header,
                //     Description =
                //         "JWT Authorization header using the Bearer scheme. Get AuthController token from Customer service. Enter token in Values eg. Bearer {token}"
                // });
                //
                // c.AddSecurityRequirement(new OpenApiSecurityRequirement
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = "Bearer"
                //             }
                //         },
                //         new string[] { }
                //     }
                // });
            });
            services.AddDbContext<ForecastContext>(options =>
                options.UseNpgsql(Configuration["postgres:connectionString"]));

            services.AddMediatR(AppDomain.CurrentDomain.Load("Service"));

            services.AddHangfire(configuration =>
            {
                configuration.UsePostgreSqlStorage(Configuration["postgres:connectionString"]);
                configuration.UseMediatR();
            });

            services.AddHangfireServer();
        }


        protected void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });
            app.UseHsts();
            app.UseCors("AllowOrigin");
            app.UseHttpsRedirection();
            app.UseApiResponseAndExceptionWrapper();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Et&T Forecast API V1"); });
        }
    }
}