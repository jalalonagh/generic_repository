using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace WebFramework.Swagger
{
    public class SetVersionInPaths : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //var updatedPaths = new Dictionary<string, OpenApiPathItem>();
            var updatePaths = new OpenApiPaths();

            foreach (var entry in swaggerDoc.Paths.ToList())
            {
                updatePaths.Add(entry.Key.Replace("v{version}", swaggerDoc.Info.Version), entry.Value);
            }

            swaggerDoc.Paths = updatePaths;
        }
    }
}
