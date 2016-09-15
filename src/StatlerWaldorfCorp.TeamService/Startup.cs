using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.LocationClient;

namespace StatlerWaldorfCorp.TeamService {
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }
                
        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddMvc();
            services.AddScoped<ITeamRepository, MemoryTeamRepository>();
            services.AddScoped<ILocationClient, LocationClient.HttpLocationClient>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}