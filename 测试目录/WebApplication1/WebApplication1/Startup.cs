using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();//�����֤�м��
            app.UseRouting();//·���м�� 

            //�ϵ㣺endpoint; ���ǽ�����http�����Url�Ľ�β�ǲ��֣��ⲿ�ֻᱻ�м�����д���
            // /{controller}/{action}
            // /home/index
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "defalut",
                    "{controller=home}/{action=index}/{id?}"
                    );
                //endpoints.MapControllers();
            });
        }
    }
}
