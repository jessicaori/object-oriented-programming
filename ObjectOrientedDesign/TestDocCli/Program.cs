using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.Extensions;

try
{
  HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

  builder.Services.Configure<AppSettings>(builder.Configuration);
  builder.Services.AddServices();
  builder.Services.AddErrorServices();
  builder.Services.AddHostedService<AppHostedService>();

  using IHost host = builder.Build();
  await host.RunAsync();
}
catch (KnownUserErrorException knownUserError)
{
  var reporter = new ErrorReporter();
  reporter.Write("ERROR", knownUserError.Message);
  Environment.ExitCode = (int)knownUserError.ExitCode;
}
catch (Exception exception)
{
  var reporter = new ErrorReporter();
  reporter.Write("UNEXPECTED", exception.Message);
  Environment.ExitCode = 1;
}

