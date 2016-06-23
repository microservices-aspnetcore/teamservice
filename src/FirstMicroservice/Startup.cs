using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace StatlerWaldorfCorp.FirstMicroservice {
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }
                
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello, world!");
            });
        }
    }   
}