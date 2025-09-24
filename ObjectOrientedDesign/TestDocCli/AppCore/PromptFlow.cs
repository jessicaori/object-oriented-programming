using TestDocCli.InputOutput;
using TestDocCli.Model;

namespace TestDocCli.AppCore;

public static class PromptFlow
{
  public static TestDocument CollectTestDocument(IInputReader inputReader, IConsole console)
  {
    console.WriteLine("Test Documentation Generator\n");

    string title = inputReader.ReadRequired("Title");
    string description = inputReader.ReadRequired("Description");

    List<string> steps = [];
    console.WriteLine("Enter Steps (one per line). Submit an empty line to finish:");

    int stepNumber = 1;

    while (true)
    {
      string prompt = $"Step {stepNumber}: ";
      string line = inputReader.ReadLine(prompt);

      if (string.IsNullOrWhiteSpace(line))
      {
        break;
      }

      steps.Add(line.Trim());
      stepNumber++;
    }

    string expected = inputReader.ReadRequired("Expected Result");
    string actual = inputReader.ReadRequired("Actual Result");

    return new TestDocument
    {
      Title = title.Trim(),
      Description = description.Trim(),
      Steps = steps,
      Expected = expected.Trim(),
      Actual = actual.Trim(),
      CreatedAt = DateTimeOffset.Now
    };
  }
}