using TestDocCli.AppCore;
using TestDocCli.Formatters;
using TestDocCli.InputOutput;
using TestDocCli.Model;

try
{
  string formatedArgument = Arguments.ReadOption(args, "--format") ?? string.Empty;
  IOutputFormatter formatter = FormatterFactory.Create(formatedArgument);

  IConsole console = new SystemConsole();
  IInputReader inputReader = new ConsoleInputReader(console);

  TestDocument testDocument = PromptFlow.CollectTestDocument(inputReader, console);

  string output = formatter.Format(testDocument);

  console.WriteLine(output);
}
catch (Exception exception)
{
  Console.Error.WriteLine("ERROR: " + exception.Message);
  Environment.ExitCode = 1;
}

// We are defining the following
// WE have a IConsole where we can define the console operations for write and read
//  - The read opeartion might complex so, we created a different class that will implement IConsole (or not) to perform the read operation

// TODO: Create the diagram of this project.