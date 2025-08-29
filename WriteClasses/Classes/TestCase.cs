namespace Classes;

public class TestCase
{
  public required int Id { get; init; }
  public required string Name { get; set; }
  public required IEnumerable<string> Steps { get; set; }

  public void Execute()
  {
    Console.WriteLine($"Executing Test Case: {Name}");
    Console.WriteLine($"Steps: \n {string.Join('\n', Steps)} \n");
    Console.WriteLine($"Result: PASS✅");
    // Id = 12; // This cannot be modified because of the init
  }
}