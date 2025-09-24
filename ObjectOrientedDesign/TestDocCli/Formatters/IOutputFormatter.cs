using TestDocCli.Model;

namespace TestDocCli.Formatters;

public interface IOutputFormatter
{
  string Format(TestDocument document);
}