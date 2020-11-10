using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingApi.Data;
using ShoppingApi.Profiles;
using ShoppingApi.Services;
using System.Net.Security;

namespace ShoppingApi
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
            services.AddDbContext<ShoppingDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("shopping"));
            });

            services.AddScoped<ILookupProducts, EfSqlShopping>();
            services.AddScoped<IPerformProductCommands, EfSqlShopping>();


            var pricingConfig = new PricingConfiguration();
            //Hydrate the configuration object
            Configuration.GetSection(pricingConfig.SectionName).Bind(pricingConfig);
            // Makes this injectable into services using IOptions<T>
            services.Configure<PricingConfiguration>(Configuration.GetSection(pricingConfig.SectionName));

            var mapperConfig = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new ProductProfile(pricingConfig));
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
            services.AddSingleton<MapperConfiguration>(mapperConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
