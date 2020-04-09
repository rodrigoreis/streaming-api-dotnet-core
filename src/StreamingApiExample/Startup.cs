namespace StreamingApiExample
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddControllersAsServices()
                .AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; });
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddControllersAsServices()
                .AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; });
        }

        public void ConfigureProduction(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
