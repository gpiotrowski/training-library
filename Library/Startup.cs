using System;
using Library.Core.Infrastructure;
using Library.Items.Services.Services;
using Library.Leases.Application.Services;
using Library.Leases.Domain.Events;
using Library.Leases.Domain.Stores;
using Library.Leases.Infrastructure.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library
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
            services.AddControllers();

            services.AddSingleton<IReaderStore, ReaderStore>();
            services.AddSingleton<Items.Infrastructure.Stores.ItemStore>();

            services.AddTransient<ItemService>();
            services.AddTransient<GetReaderLeasesService>();
            services.AddTransient<LeaseBookService>();

            services.AddSingleton<IEventPublisher, InMemoryEventPublisher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var eventPublisher = app.ApplicationServices.GetService<IEventPublisher>();
            eventPublisher.Subscribe<BookLeased>(e => Console.WriteLine($"$Book leased for bookId = {e.BookId}"));
        }
    }
}
