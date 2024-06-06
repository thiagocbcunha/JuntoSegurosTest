using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JuntoSeguros.Enterprise.Library.Tracing.Extensions;

public static class EnterpriseTracingBuilderExtension
{
    public static EnterpriseTracingBuilder CreateEnterpriseTracingBuilder(this IServiceCollection service, IConfiguration configuration) 
        => new EnterpriseTracingBuilder(service, configuration);
}
