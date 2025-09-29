using TestDocCli.AppCore;
using TestDocCli.Formatters;

namespace TestDocCli.Tests.AppCore;

public class OutputFormatterFactoryTests
{
  [Fact]
  public void Create_KnownHtmlFormat_ReturnsHtmlFormatter()
  {
    var factory = new OutputFormatterFactory();

    IOutputFormatter formatter = factory.Create("html");

    Assert.IsType<HtmlFormatter>(formatter);
  }
}
