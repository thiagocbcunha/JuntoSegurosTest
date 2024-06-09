using JuntoSeguros.Infra.IoC;
using JuntoSeguros.Enterprise.Library.Security;
using JuntoSeguros.Enterprise.Library.Logging.Extensions;
using JuntoSeguros.Enterprise.Library.Tracing.Extensions;

var builder = WebApplication.CreateBuilder(args);

var environmentName = builder.Environment.EnvironmentName;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEnterpriseSecurity();
builder.Services.AddEndpointsApiExplorer();

builder.Logging.ConfigureEnterpriceLog(builder.Configuration, "ApplicationName");

var enterpriseTracingBuilder = builder.Services.CreateEnterpriseTracingBuilder(builder.Configuration);
enterpriseTracingBuilder.AddSQLInstrumentation();
enterpriseTracingBuilder.BuildService();

builder.Services.SetupApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment() || environmentName.Equals("docker", StringComparison.InvariantCultureIgnoreCase))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
