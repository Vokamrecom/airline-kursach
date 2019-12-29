using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Airline.DataProvider;
using Airline.DataProvider.Repositories;
using Airline.Logic.Services;
using ReflectionIT.Mvc.Paging;
using Swashbuckle.AspNetCore.Swagger;

namespace Airline.Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string connection = Configuration.GetConnectionString("Airline");
            services.AddDbContext<AirlineContext>(options => options.UseSqlServer(connection));

            services.AddTransient<IAircraftRepository, AircraftRepository>();
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IFlightRepository, FlightRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IPassengerRepository, PassengerRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();

            services.AddTransient<IAircraftService, AircraftService>();
            services.AddTransient<IAirportService, AirportService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IPassengerService, PassengerService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ICommitService, CommitService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ErmakoffAirline API", Version = "1.0.0" });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            app.UseSession();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
