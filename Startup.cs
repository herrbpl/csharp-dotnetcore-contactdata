using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

 
using Microsoft.Extensions.Configuration.Json;


using ASTV.Services;
using ASTV.Models.Employee;

namespace ASTVWebApi
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)        
        {

            string eDbConfig = Configuration.GetConnectionString("EmployeeDatabase") != null?
                                    Configuration.GetConnectionString("EmployeeDatabase"):
                                    @"Server=TISCALA.NTSERVER2.SISE;Database=scalaDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            string cDbConfig = Configuration.GetConnectionString("ContactDataDatabase") != null?
                                    Configuration.GetConnectionString("ContactDataDatabase"):
                                    @"Server=(localdb)\mssqllocaldb;Database=FuckYou;Trusted_Connection=True;MultipleActiveResultSets=True";            

            services.AddDbContext<ContactDataContext>(options => 
                options.UseSqlServer(cDbConfig)
            );

             services.AddDbContext<EmployeeContext>(options => 
                options.UseSqlServer(eDbConfig)
            );

                                        

            // try add LDAP context.
            var ldapServerConfigurationSection = Configuration.GetSection(nameof(LdapServerConfiguration));
            var ldapServerConfiguration = new LdapServerConfiguration();            
            ldapServerConfigurationSection.Bind(ldapServerConfiguration);
            services.Add(new ServiceDescriptor(typeof(ILDAPContext<Employee>),
                 p => new LDAPContext<Employee>(ldapServerConfiguration,  new LoggerFactory()
                            .AddConsole()
                            .AddDebug()
                ), ServiceLifetime.Scoped));


            // default contact data provider
            services.AddScoped<IContactDataDefaultProvider, ContactDataTestProvider>();

            // Language repository
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            // Education Repository
            services.AddScoped<IEducationRepository, EducationRepository>();
            // Employee Repository
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            // ContactData Repository
            services.AddScoped<IContactDataRepository, ContactDataRepository>();
            // 
            services.AddCors();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            /*
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            */
            app.UseMvc();
        }
    }
}
