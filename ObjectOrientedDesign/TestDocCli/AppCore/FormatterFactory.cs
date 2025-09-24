using TestDocCli.Formatters;

namespace TestDocCli.AppCore;

public static class FormatterFactory
{
  public static IOutputFormatter Create(string formatArgument)
  {
    string normalized = formatArgument.Trim().ToLowerInvariant();

    return normalized switch
    {
      "md" or "markdown" => new MarkdownFormatter(),
      "html" => new HtmlFormatter(),
      "console" or "text" => new PlainConsoleFormatter(),
      _ => new PlainConsoleFormatter()
    };
  }
}