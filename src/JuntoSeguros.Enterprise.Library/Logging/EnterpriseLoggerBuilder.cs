using Serilog;
using Serilog.Filters;
using Serilog.Exceptions;
using Microsoft.Extensions.Configuration;
using JuntoSeguros.Enterprise.Library.Options;

namespace JuntoSeguros.Enterprise.Library.Logging;

internal class EnterpriseLoggerBuilder
{
    private readonly IConfiguration _configuration;
    private readonly LoggerConfiguration _loggerConfiguration;

    public EnterpriseLoggerBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
        _loggerConfiguration = new LoggerConfiguration();            
    }

    public EnterpriseLoggerBuilder ApplyFilter()
    {
        _loggerConfiguration
            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("Microsoft.AspNetCore")));

        return this;
    }

    public EnterpriseLoggerBuilder AddEnrich()
    {
        _loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationId()
            .Enrich.WithExceptionDetails()
            .Enrich.WithCorrelationIdHeader();

        return this;
    }

    public EnterpriseLoggerBuilder AddName(string appNameConfigurationSection)
    {
        var applicationName = _configuration.GetValue<string>(appNameConfigurationSection);

        _loggerConfiguration
            .Enrich.WithProperty("ApplicationName", applicationName);

        return this;
    }

    public EnterpriseLoggerBuilder WriteToConsole()
    {
        _loggerConfiguration.WriteTo.Async(wt => wt.Console());
        return this;
    }

    public EnterpriseLoggerBuilder WriteToLogstash()
    {
        var logSettings = new EnterpriceConfiguration();
        _configuration.GetRequiredSection(nameof(EnterpriceConfiguration)).Bind(logSettings);
        _loggerConfiguration.WriteTo.Http(logSettings.LogstashEndpoint, queueLimitBytes: null);
        return this;
    }

    public ILogger Build()
    { 
        return _loggerConfiguration.CreateLogger();
    }
}