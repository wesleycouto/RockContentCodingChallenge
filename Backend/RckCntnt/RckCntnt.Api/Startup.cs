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
using System;

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
                    var rabbitMqQueueName = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE_NAME");
                    var rabbitMqUsername = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
                    var rabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
                    var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");

                    cfg.ReceiveEndpoint(rabbitMqQueueName, endpointConfig =>
                    {
                        endpointConfig.ConfigureConsumer<LikeConsumer>(context);
                        endpointConfig.ConcurrentMessageLimit = 1;
                    });

                    cfg.Host(rabbitMqHost, "/", host =>
                    {
                        host.Username(rabbitMqUsername);
                        host.Password(rabbitMqPassword);
                    });
                });
            });

            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddAutoMapper(c => c.AddMaps(typeof(AutoMapperConfig).Assembly));

            services.AddMassTransitHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RckCntnt Api",
                    Version = "v1",
                    Description = "RockContent Challenge Api",
                    Contact = new OpenApiContact
                    {
                        Email = "wesleycouto@gmail.com",
                        Name = "Wesley Couto da Silva",
                        Url = new Uri("https://github.com/wesleycouto/RockContentCodingChallenge/")
                    }
                });
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