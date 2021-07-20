using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HollywoodAssessment.Common.Interfaces;
using HollywoodAssessment.Common.Helper;
using HollywoodAssessment.Data.Models;
using HollywoodAssessment.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using HollywoodAssessment.Common.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HollywoodAssessment
{
  public class Startup
  {
    public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddDbContext<HollywoodAssessmentDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("HollywoodAssessmentDb")));
      services.AddTransient<ITournamentService, TournamentService>();
      services.AddTransient<IEventsService, EventService>();
      services.AddTransient<IEventDetailService, EventDetailsService>();
      services.AddTransient<IUserService, UserService>();
      services.AddCors();
      services.AddAutoMapper();

      services.AddSwaggerGen(x =>
      {
        x.SwaggerDoc("v1", new Info
        {
          Title = "Hollywood",
          Version = "v1",
          Description = "To be updated",
          Contact = new Contact
          {
            Email = "sbuddaz@gmail.com",
            Name = "Sibusiso Sikhakhane"
          }
        });
      });

      var appsetSection = Configuration.GetSection("AppSetting");
      services.Configure<appSettings>(appsetSection);

      var appsettings = appsetSection.Get<appSettings>();
      var key = Encoding.ASCII.GetBytes(appsettings.Secret);
      services.AddAuthentication(x =>
        {
          x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
          x.Events = new JwtBearerEvents
          {
            OnTokenValidated = context =>
            {
              var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
              var userId = int.Parse(context.Principal.Identity.Name);
              var user = userService.GetById(userId);
              if (user == null)
              {
                // return unauthorized if user no longer exists
                context.Fail("Unauthorized");
              }
              return Task.CompletedTask;
            }
          };
          x.RequireHttpsMetadata = false;
          x.SaveToken = true;
          x.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
          };
        });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseStaticFiles();
      app.UseSwagger();
      app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Hollywood API v1"); } );
      app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
      app.UseAuthentication();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
