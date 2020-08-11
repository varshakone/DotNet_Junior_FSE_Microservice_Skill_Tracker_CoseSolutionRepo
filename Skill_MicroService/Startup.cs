﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Skill_MicroService.BusinessLayer.Interface;
using Skill_MicroService.BusinessLayer.Service;
using Skill_MicroService.BusinessLayer.Service.Repository;
using Skill_MicroService.DataLayer;

namespace Skill_MicroService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<MongoSettings>(options => {

                options.DatabaseName = Configuration.GetSection("MongoConnection:DatabaseName").Value;
                options.Connection = Configuration.GetSection("MongoConnection:Connection").Value;

                if (options.Connection == null && options.DatabaseName == null)
                {
                    options.Connection =
                    "mongodb://user:password@127.0.0.1:27017/guestbook";
                    options.DatabaseName = "guestbook";
                }
            }
            );
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IMongoDBContext, MongoDBContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
