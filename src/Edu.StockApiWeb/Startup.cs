namespace Edu.StockApiWeb;

public class Startup
{
    public void ConfigureServices(IServiceCollection service)
    {
        
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { });
    }
}