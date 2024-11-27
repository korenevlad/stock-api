using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Edu.StockApi.Web;

//  Добавляет новые параметры к запросу 
public abstract class HeaderOperationFilter: IOperationFilter
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