using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateFeedNotificationService.Hubs;
using Microsoft.AspNetCore.SignalR;
using RateFeedNotificationService.MessageHandlers;
using RateFeedNotificationService.Bus;
using RateFeedNotificationService.Hubs.Notifiers;

namespace RateFeedNotificationService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<RateFeedSubscriptionSettings>();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static IHubContext<RateFeedHub> RateFeedHubContext;
        public static string Key { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddScoped<INotifyHubClient, HubClientNotifier>();
            services.AddSingleton<IProvideRateFeedBusSettings>(p=>new RateFeedSubscriptionSettings(Configuration["payments-servicebus-shared-accesskey"]));
            services.AddScoped<IHandleRateFeedSubscriptionMessage, RateFeedSubscriptionHandler>();
            services.AddScoped<IRegisterHandlers, RegisterHandlers>();
            services.AddMvc();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAllOrigins");
            app.UseSignalR(routes => routes.MapHub<RateFeedHub>("rates-feed-hub"));
            RateFeedHubContext = serviceProvider.GetService<IHubContext<RateFeedHub>>();
            serviceProvider.GetService<IRegisterHandlers>().Register();
            app.UseMvc();
        }
    }
}
