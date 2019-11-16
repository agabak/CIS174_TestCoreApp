using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Filters;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CIS174_TestCoreApp
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
 
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<UserPerson>(confiq => 
                {
                   confiq.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policyBuilder => policyBuilder.AddRequirements(
                                                 new MinimumAgeRequirement(18),
                                                 new IsActiveUser()));
                options.AddPolicy("CanEditContent", policyBuilder => policyBuilder.AddRequirements(new ContentEditorRequirement()));

                options.AddPolicy("editor", policyBuilder => policyBuilder.RequireClaim("ContentEditor"));
            });

            services.AddScoped<IAuthorizationRequirement, IsActiveUser>();
            services.AddScoped<IAuthorizationHandler, IsActiveUserHandler>();
            services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddScoped<IAuthorizationHandler, ContentEditorHandler>();
            //services.AddIdentity<UserPerson, UserRole>(config =>
            //{  AuthorizationPolicyBuilder
            //    config.User.RequireUniqueEmail = true;

            //}).AddEntityFrameworkStores<DataContext>(); 

            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IAccomplishmentService, AccomplishmentService>();
            services.AddScoped<IPersonManagerService, PersonManagerService>();
           
            services.AddScoped<ValidateModelAttribute>();

            services.AddMvc(config =>
                    {
                        config.Filters.Add(typeof(CustomExceptionFilter));
                    }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
           
            app.UseMvc(routes =>
            {
                routes.MapRoute("Alternative", 
                                 "{controller}/{action}/{id?}",
                              new {controller ="Home", action ="Index", id ="" });
            });
        }
    }
}
