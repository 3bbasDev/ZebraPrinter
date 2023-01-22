using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ZebraPrinterSDK;
using ZebraPrinterSDK.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Trace);
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .UseWindowsService()
    .Build();
    
    //.ConfigureServices(services =>
    //{
        
    //    services.AddHostedService<Worker>();
    //})
    //.ConfigureWebHostDefaults(webBuilder =>
    //{
    //    webBuilder.UseStartup<Startup>();
    //})
    //.UseWindowsService()
    //.Build();

await host.RunAsync();


