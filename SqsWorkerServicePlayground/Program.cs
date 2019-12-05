using Amazon;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SqsWorkerServicePlayground
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var options = hostContext.Configuration.GetAWSOptions();
                    options.Region = RegionEndpoint.EUWest2;

                    services.AddDefaultAWSOptions(options);
                    services.AddAWSService<IAmazonSQS>();

                    services.AddHostedService<Worker>();
                });
    }
}
