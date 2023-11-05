using Serilog;

namespace API.Extensions
{
    public static class SerilogConfiguration
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
           .ReadFrom.Configuration(builder.Configuration)
           .Enrich.FromLogContext()
           .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            return builder;
        }
    }
}
