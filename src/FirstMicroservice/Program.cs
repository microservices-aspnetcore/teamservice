using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace StatlerWaldorfCorp.FirstMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
            	.Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<StatlerWaldorfCorp.FirstMicroservice.Startup>()
                .UseConfiguration(config)
                .Build();

            host.Run();            
        }
    }
}
