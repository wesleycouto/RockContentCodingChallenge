using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RckCntnt.Api.Consumers;
using RckCntnt.Application.AutoMapper;
using RckCntnt.BootStrappers;

namespace RckCntnt.Api
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
            services.AddControllers();

            services.AddMassTransit(busConfig =>
            {
                busConfig.AddConsumer<LikeConsumer>();
                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("likes_queue", endpointConfig => endpointConfig.ConfigureConsumer<LikeConsumer>(context));

                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                });
            });

            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddAutoMapper(c => c.AddMaps(typeof(AutoMapperConfig).Assembly));

            services.AddMassTransitHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RckCntnt Api", Version = "v1", Description = "RockContent Challenge Api" });
            });

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RckCntnt Api"));
            }

            app.UseRouting();

            app.UseCors(option =>
            {
                option.AllowAnyMethod();
                option.AllowAnyHeader();
                option.AllowAnyOrigin();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            RckCntntBootStrapper.RegisterServices(services);
        }
    }
}