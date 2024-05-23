using IGeekFan.AspNetCore.RapiDoc;
using IO.Swagger.Filters;
using IO.Swagger.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IO.Swagger
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnv;

        private IConfiguration Configuration { get; }
        
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _hostingEnv = env;
            Configuration = configuration;
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.Get<AppSettings>();
            services.AddSingleton(config);

            services.AddControllers();

            services.AddMvc(options =>
            {
                options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                options.UseGeneralRoutePrefix(config?.SwaggerSettings?.ApiRoutePrefix);
            })
            .AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            })
            .AddXmlSerializerFormatters();

            services.AddHealthChecks();

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "VV";
                options.SubstituteApiVersionInUrl = true;
                options.SubstitutionFormat = "VV";
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName);
                c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");
                // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                c.OperationFilter<GeneratePathParamsValidationFilter>();
                c.EnableAnnotations();
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            var config = Configuration.Get<AppSettings>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHealthChecks("/health");

            app.UseSwagger(c =>
            {
                c.RouteTemplate = config.SwaggerSettings.SwaggerPrefix + "/{documentname}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = config.SwaggerSettings.SwaggerPrefix;
                foreach(var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/{config.SwaggerSettings.SwaggerPrefix}/{description.GroupName}/swagger.json", $"Petstore {description.GroupName}");
                }
            });

            app.UseReDoc(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions) 
                {
                    c.RoutePrefix = $"{config.SwaggerSettings.ApiRoutePrefix}api-docs-{description.GroupName}";
                    c.DocumentTitle = $"Petstore {description.GroupName}";
                    c.SpecUrl($"/{config.SwaggerSettings.SwaggerPrefix}/{description.GroupName}/swagger.json");
                }               
                
                c.EnableUntrustedSpec();
                c.ScrollYOffset(10);
                c.HideDownloadButton();
                c.RequiredPropsFirst();
                c.NoAutoAuth();
                c.HideLoading();
                c.NativeScrollbars();
                c.SortPropsAlphabetically();
            });

            app.UseRapiDocUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.RoutePrefix = $"{config.SwaggerSettings.ApiRoutePrefix}rapi-docs-{description.GroupName}";
                    c.DocumentTitle = $"Petstore {description.GroupName}";
                    c.SwaggerEndpoint($"/{config.SwaggerSettings.SwaggerPrefix}/{description.GroupName}/swagger.json", $"Petstore {description.GroupName}");
                }                
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
