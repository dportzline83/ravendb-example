using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using RavenExample.Data;

namespace RavenExample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<DocumentStoreLifecycle>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseWelcomePage();
        }
    }
}
