using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neuroglia.AsyncApi;
using Neuroglia.AsyncApi.Models.Bindings.Mqtt;
using StreetLightsApi.Server.Services;
using System;

namespace StreetLightsApi.Server
{

    public class Startup
    {

        public Startup(IWebHostEnvironment environment)
        {
            this.Environment = environment;
        }

        IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAsyncApiUI();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            services.AddControllers(); 
            services.AddAsyncApiGeneration(builder => 
                builder.WithMarkupType<StreetLightsService>()
                    .UseDefaultConfiguration(asyncApi =>
                    {
                        asyncApi
                            .WithTermsOfService(new Uri("https://www.websitepolicies.com/blog/sample-terms-service-template"))
                            .UseServer("mosquitto", server => server
                                .WithUrl(new Uri("mqtt://test.mosquitto.org"))
                                .WithProtocol(AsyncApiProtocols.Mqtt)
                                .WithDescription("The Mosquitto test MQTT server")
                                .UseBinding(new MqttServerBindingDefinition()
                                {
                                    ClientId = "StreetLightsAPI:1.0.0",
                                    CleanSession = true
                                }));
                    }));

            services.AddSingleton<StreetLightsService>();
            services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<StreetLightsService>());

            services.AddSingleton<MovementSensorService>();
            services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<MovementSensorService>());
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
                //endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
