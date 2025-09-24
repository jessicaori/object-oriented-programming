namespace TestDocCli.AppCore;

public static class Arguments
{
  public static string? ReadOption(string[] args, string name)
  {
    for (int index = 0; index < args.Length; index++)
    {
      if (string.Equals(args[index], name, StringComparison.OrdinalIgnoreCase))
      {
        int nextIndex = index + 1;

        if (nextIndex < args.Length)
        {
          return args[nextIndex];
        }
      }
      else if (args[index].StartsWith($"{name}=", StringComparison.OrdinalIgnoreCase))
      {
        return args[index].Substring(name.Length + 1);
      }
    }

    return null;
  }
}