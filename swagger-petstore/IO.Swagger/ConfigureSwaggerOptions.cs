using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace IO.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
          this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                {
                    Title = $"PetStore {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "Swagger Petstore (ASP.NET Core 6.0)",
                    Contact = new OpenApiContact()
                    {
                        Name = "Swagger Codegen Contributors",
                        Url = new Uri("https://github.com/swagger-api/swagger-codegen"),
                        Email = "apiteam@swagger.io"
                    },
                    TermsOfService = new Uri("http://swagger.io/terms/")
                });
            }
        }
    }
}
