using System.Reflection;
using Edu.StockApi.Web.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Edu.StockApi.Web;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //  Добавление контроллеров
        services.AddControllers();
        
        //   DI
        services.AddSingleton<IStockService, StockService>();

        //  добавление Swagger
        services.AddSwaggerGen(options =>
        {
            //  Описание свагера приложения
            options.SwaggerDoc("v1", new OpenApiInfo(){Title = "Edu.StockApi", Version = "v1"} );
            
            //  Добавление документации в сваггер
            var xmlFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
            options.IncludeXmlComments(xmlFilePath);

            options.OperationFilter<HeaderOperationFilter>();
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        //  Добавить сваггер
        app.UseSwagger();
        app.UseSwaggerUI();
        
        //  Добавить роутинг
        app.UseRouting();
        
        //  Добавить роутинг
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

//  Добавляет новые параметры к запросу 
public class HeaderOperationFilter: IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //  проверили, есть ли список
        operation.Parameters ??= new List<OpenApiParameter>();
        //   добавили header
        operation.Parameters.Add(new OpenApiParameter(new OpenApiParameter
        {
            In = ParameterLocation.Header,
            Name = "our-header",
            Required = false,   
            Schema = new OpenApiSchema{Type = "string"}
        }));
    }
}