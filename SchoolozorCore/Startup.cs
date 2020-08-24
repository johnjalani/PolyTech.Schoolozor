using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schoolozor.Model;
using Schoolozor.Services.Authentication.Services;
using Schoolozor.Services.SchoolYear.Services;
using Schoolozor.Services.Student.Services;
using Schoolozor.Shared;
using System.Globalization;

namespace SchoolozorCore
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environments.Development}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            //services.AddDbContext<SchoolContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<SchoolContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection"));
            });

            services.AddIdentity<SchoolUser, IdentityRole>(o =>
            {
                o.SignIn.RequireConfirmedEmail = true;
                o.User.RequireUniqueEmail = true;
                o.Password.RequiredLength = 8;
            })
             .AddEntityFrameworkStores<SchoolContext>()
             .AddRoleManager<RoleManager<IdentityRole>>()
             .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<SchoolUser>, SchoolClaimsPrincipalFactory>();
            services.AddScoped<SignInManager<SchoolUser>, AuditableSignInManager<SchoolUser>>();

            var mvcBuilder = services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.Configure<CookieAuthenticationOptions>(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
            });
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<Email>();
            services.AddTransient<StudentServices>();
            services.AddTransient<SchoolYearServices>();
            services.AddTransient(typeof(IDataManager<>), typeof(DataManager<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithRedirects("~/Home/Error/{0}");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //Add middleware here
            app.UseMiddleware<RequestLoggingMiddleware>();


            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            //do not add middleware here (it wont be invoked)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


            //var logger = loggerFactory.CreateLogger(env.EnvironmentName);
            //app.Use(async (context, next) =>
            //{
            //    await next.Invoke();
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("ill be executed after the code above!");
            //    Debug.WriteLine("invoke thru await next.Invoke();");
            //});

            // Populate default user admin
            //DataSeed.Seed(app.ApplicationServices).Wait();
        }
    }
}
