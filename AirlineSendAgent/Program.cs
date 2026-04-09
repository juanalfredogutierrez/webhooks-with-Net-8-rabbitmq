using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AirlineSendAgent.App;

var host = Host.CreateDefaultBuilder()
               .ConfigureServices((context, services) =>
               {
                   services.AddSingleton<IAppHost, AppHost>();
               }).Build();

host.Services.GetService<IAppHost>().Run();