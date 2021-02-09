using System.IO;
using System.Threading.Tasks;
using Foundatio.Messaging;
using Foundatio.Serializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Arc.Core.Distributed.Nodes.Info;
using Arc.Core.Distributed.Nodes.Messaging;
using Serilog;
using StackExchange.Redis;

namespace Arc.Service.Game
{
    public static class Program
    {
        private static async Task Main()
        {

            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(hostContext.Configuration)
                        .CreateLogger();

                    services.AddSingleton<ILogger>(Log.Logger);
                    services.AddOptions();
                    services.AddSingleton<ConnectionMultiplexer>(f =>
                        ConnectionMultiplexer.Connect("localhost"));
                    services.AddSingleton<IMessageBusFactory, RedisMessageBusFactory>();
                    services.AddSingleton<IMessageBus>(f =>
                        f.GetService<IMessageBusFactory>().Build("messages"));
                    DefaultSerializer.Instance = new JsonNetSerializer(
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        }
                    );
                    services.AddSingleton<IHostedService, GameNode>()
                        .Configure<GameServerNodeServiceInfo>(hostContext.Configuration.GetSection("Service"))
                        .AddLogging();
                })
                .UseSerilog()
                .Build();

            await host.RunAsync();
        }

    }
}
