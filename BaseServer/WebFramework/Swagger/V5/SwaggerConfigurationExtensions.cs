using Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebFramework.Swagger
{
    public static class SwaggerConfigurationExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            Assert.NotNull(services, nameof(services));

            services.AddSwaggerExamplesFromAssemblies();
            services.AddSwaggerGen(options =>
            {
                var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "Moradi.xml");

                options.IncludeXmlComments(xmlDocPath, true);
                options.CustomSchemaIds(x => x.FullName);
                options.EnableAnnotations();
                options.DescribeAllEnumsAsStrings();

                options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "API V1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "API V2" });

                #region Filters
                options.ExampleFilters();

                options.OperationFilter<ApplySummariesOperationFilter>();

                #region Add UnAuthorized to Response
                options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "Bearer");
                #endregion

                #region Add Jwt Authentication
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {             
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
#if DEBUG
                            TokenUrl = new Uri("http://localhost:56960/api/v1/users/Token", UriKind.Absolute),
#else
                            TokenUrl = new Uri("https://token.dinavision.org/api/v1/users/Token", UriKind.Absolute),

#endif
                        }
                    }
                });
#endregion

#region Versioning
                options.OperationFilter<RemoveVersionParameters>();

                options.DocumentFilter<SetVersionInPaths>();

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });
#endregion

#endregion
            });
        }

        public static void UseSwaggerAndUI(this IApplicationBuilder app)
        {
            Assert.NotNull(app, nameof(app));

            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(options =>
            {
#region Customizing
                options.DocExpansion(DocExpansion.None);
#endregion
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");
            });
        }
    }
}
