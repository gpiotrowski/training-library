using Library.Items.Services.Services;
using Library.Orders.Infrastructure.Stores;
using Library.Orders.Services.Services;
using Library.Orders.Services.Stores;
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

            services.AddSingleton<IUserStore, UserStore>();
            services.AddSingleton<IOrderStore, OrderStore>();
            services.AddSingleton<Items.Infrastructure.Stores.ItemStore>();

            services.AddTransient<ItemService>();
            services.AddTransient<OrderService>();
            services.AddTransient<IItemStore, Orders.Infrastructure.Stores.ItemStore>();
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
        }
    }
}
