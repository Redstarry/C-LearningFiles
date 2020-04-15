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
            //ע�����ݿ��������
            services.AddTransient<IContactRepository,ContactRepository>();
            services.AddTransient<IHangFireCRUD, HangFireCRUD>();
            //HangFire ����ע��
            services.AddHangfire(options => options.UseSqlServerStorage("server=.; uid = sa; pwd = 123; database = ContactInformation"));
            //Mapper(���ݿ�ʵ����DTO֮���ӳ��)
            services.AddAutoMapper(typeof(Startup)); //AppDomain.CurrentDomain.GetAssemblies()
            //API �ĵ���ע��
            services.AddSwaggerGen(option =>
            {

                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My Api",
                    Version = "v1"
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(basePath, "SwaggerDemo.xml");
                option.IncludeXmlComments(xmlPath);
            });
            services.Configure<ConnectionConfig>(Configuration.GetSection("ConnectionStrings"));
            //��������Core����ע��
            services.AddCors(option => option.AddPolicy("Domain", builder=>builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            // JWT ����ע��
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearToken"; //Ĭ�ϵ������֤ģʽ
                options.DefaultChallengeScheme = "JwtBearToken";
                options.DefaultScheme = "JwtBearToken"; //Ĭ��ģʽ

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

            app.UseStaticFiles(); //ʹ��html��js���м��
            
            // ��http����ת����https����
            app.UseHttpsRedirection();
            app.UseRouting();
            
            //��������
            app.UseCors("Domain");

            //JWT����
            app.UseAuthentication();
            app.UseAuthorization();

            // HangFire ���� ("/hangfire" ����URLӳ��)
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                IsReadOnlyFunc = (DashboardContext context) => false // ����ֻ����ͼ��Ĭ���ǹر�
            }) ;
            app.UseHangfireServer(new BackgroundJobServerOptions
            { 
                Queues = new[] { "default"}            
            });

            // API�ĵ�����
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
