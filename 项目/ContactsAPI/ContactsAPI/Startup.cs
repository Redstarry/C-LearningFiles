using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.IO;
using ContactsAPI.Models.config;
using ContactsAPI.Models.DataRepository;
using ContactsAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using Hangfire.Dashboard;
using ContactsAPI.Models.HangfireInfo;
using ContactsAPI.Models.Quartz;
using Quartz.Impl;
using Quartz;
using StackExchange;
using ContactsAPI.Models.Redis;

namespace ContactsAPI
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
            services.AddControllers();
            //注册数据库操作服务
            services.AddTransient<IContactRepository,ContactRepository>();
            services.AddTransient<IHangFireCRUD, HangFireCRUD>();
            services.AddTransient<IQuartzServer, QuartzServer>();
            services.AddTransient<ISchedulerFactory, StdSchedulerFactory>(); //使用Quartz
            //HangFire 服务注册
            services.AddHangfire(options => options.UseSqlServerStorage("server=.; uid = sa; pwd = 123; database = ContactInformation"));
            //Mapper(数据库实体与DTO之间的映射)
            services.AddAutoMapper(typeof(Startup)); //AppDomain.CurrentDomain.GetAssemblies()
            //API 文档的注册
            services.AddSwaggerGen(option =>
            {

                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My Api",
                    Version = "v1"
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "SwaggerDemo.xml");
                option.IncludeXmlComments(xmlPath);
            });
            services.Configure<ConnectionConfig>(Configuration.GetSection("ConnectionStrings"));
            //解决跨域的Core服务注册
            services.AddCors(option => option.AddPolicy("Domain", builder=>builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            // JWT 服务注册
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearToken"; //默认的身份验证模式
                options.DefaultChallengeScheme = "JwtBearToken";
                options.DefaultScheme = "JwtBearToken"; //默认模式

            }).AddJwtBearer("JwtBearToken", options => {
                //options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    SaveSigninToken = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789963258741")),

                    ValidateIssuer = true,
                    ValidIssuer = "tp",

                    ValidateAudience = true,
                    ValidAudience = "everyone",

                    ValidateLifetime = true,

                    //ClockSkew = TimeSpan.FromMinutes(5)

                };
                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); //使用html、js的中间件
            
            // 把http请求转换成https请求
            app.UseHttpsRedirection();
            app.UseRouting();
            
            //跨域配置
            app.UseCors("Domain");

            //JWT配置
            app.UseAuthentication();
            app.UseAuthorization();

            // HangFire 配置 ("/hangfire" 更改URL映射)
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                IsReadOnlyFunc = (DashboardContext context) => false // 开启只读视图，默认是关闭
            }) ;
            app.UseHangfireServer(new BackgroundJobServerOptions
            { 
                //Queues = new[] { "default"},
                ShutdownTimeout = TimeSpan.FromMinutes(30),
                Queues = new[] { "default" },
                WorkerCount = Math.Max(Environment.ProcessorCount,20)


            });

            // API文档配置
            app.UseSwagger();
            app.UseSwaggerUI(option => {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
