using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neuroglia.AsyncApi.Samples.StreetLightsApi.Services;
using System;

namespace Neuroglia.AsyncApi.Samples.StreetLightsApi
{

    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        IConfiguration Configuration { get; }

        IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAsyncApiGeneration(builder => 
                builder.WithMarkupType<StreetLightsService>()
                    .UseDefaultConfiguration(asyncApi =>
                    {
                        asyncApi.UseServer("mosquitto", server => server
                            .WithUrl(new Uri("mqtt://test.mosquitto.org"))
                            .WithProtocol(AsyncApiProtocols.Mqtt));
                    }));
            services.AddSingleton<StreetLightsService>();
            services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<StreetLightsService>());
        }

        public void Configure(IApplicationBuilder app)
        {
            if (this.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAsyncApiGeneration();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
