using FluentValidation.AspNetCore;
using Gram.Application.Events.Commands.CreateEvent;
using Gram.Application.Interfaces;
using Gram.Persistence;
using Gram.Persistence.Services;
using Gram.Web.Areas.Identity;
using Gram.Web.Areas.Identity.Models;
using Gram.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gram.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AuditContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<WebUser>().AddDefaultUI(UIFramework.Bootstrap4).AddEntityFrameworkStores<IdentityContext>();

            services.AddScoped<IAuditContext, AuditContext>();
            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped<IUserService, UserService>();

            services.AddMvc().AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<CreateEventCommandValidator>());
            services.AddMediatR(typeof(CreateEventCommand).GetTypeInfo().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                InitializeDatabase(app);
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<AuditContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<DataContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<IdentityContext>().Database.Migrate();
            }
        }
    }
}
