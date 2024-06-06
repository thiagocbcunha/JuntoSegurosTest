using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using Microsoft.Extensions.Configuration;
using JuntoSeguros.Enterprise.Library.Options;
using Microsoft.Extensions.DependencyInjection;
using JuntoSeguros.Enterprise.Library.Contracts;

namespace JuntoSeguros.Enterprise.Library.Tracing;

public class EnterpriseTracingBuilder
{
    private readonly IServiceCollection _service;
    private readonly IConfiguration _configuration;
    Action<TracerProviderBuilder> _providerBuilderAction = i => { };

    internal EnterpriseTracingBuilder(IServiceCollection service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;

        var enterpriseConfiguration = new EnterpriceConfiguration();
        _configuration.GetRequiredSection(nameof(EnterpriceConfiguration)).Bind(enterpriseConfiguration);

        _service.AddScoped(i => enterpriseConfiguration);
        _service.AddScoped<IActivityFactory, EnterpriseActivityFactory>();

        _providerBuilderAction += i => i
            .AddSource(enterpriseConfiguration.Name)
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(serviceName: enterpriseConfiguration.Name, serviceVersion: enterpriseConfiguration.Version));

        _providerBuilderAction += i => i.AddHttpClientInstrumentation();
        _providerBuilderAction += i => i.AddAspNetCoreInstrumentation();
        _providerBuilderAction += i => i.AddOtlpExporter(opts => opts.Endpoint = new Uri(enterpriseConfiguration.OTELEndpoint));
    }

    public void AddSQLInstrumentation()
    {
        _providerBuilderAction += i => i.AddSqlClientInstrumentation(options => options.SetDbStatementForText = true);
    }

    public void AddEFInstrumentation()
    {
        _providerBuilderAction += i => i.AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true);
    }

    public void BuildService()
    {
        _service.AddOpenTelemetry().WithTracing(_providerBuilderAction);
    }
}