using Edu.StockApiWeb;

CreatingHostBuilder(args).Build().Run();

static IHostBuilder CreatingHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>();});