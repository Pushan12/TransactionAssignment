namespace IO.Swagger
{
    public class AppSettings
    {
        public SwaggerSettings SwaggerSettings { get; set; }
    }

    public class SwaggerSettings
    {
        public string ApiRoutePrefix { get; set; }
        public string SwaggerPrefix { get; set; }
    }
}
