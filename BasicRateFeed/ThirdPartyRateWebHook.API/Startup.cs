using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThirdPartyRateWebHook.API.Managers;
using ThirdPartyRateWebHook.API.MessageSenders;
using ThirdPartyRateWebHook.API.ServiceProvider;

namespace ThirdPartyRateWebHook.API
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
            services.AddMvc();
            services.AddScoped<IPublishThirdPartyRate, AzureServiceBusMessageSender>();
            services.AddSingleton<IThirdPartyRatesCommandRA, MemoryPeristence>();
            services.AddScoped<IThirdPartyRateManager, ThirdPartyRatesManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
