namespace ClassesAndObjects
{
  /// <summary>
  /// Example - Class basics and benefits
  /// Imagine your team is automating test cases for login functionality.
  /// - Instead of writing login steps repeatedly in every test, you define a LoginTest class.
  /// - That class can have attributes like Username and Password, and methods like Execute() to run the login test.
  /// 
  /// Example - constructors
  /// Image a TestCase class. Each test needs:
  /// - A name
  /// - A priority
  /// - A status
  /// We can use a constructor to set these up at the moment the test case object is created.
  /// </summary>

  public class LoginTest
  {
    // Attributes (fields/properties)
    public required string Username { get; init; }
    public required string Password { get; init; }

    // Method (behavior)
    public bool Execute()
    {
      Console.WriteLine($"Executing login with user: {Username}");

      return Username == "qa_user" && Password == "secure_password";
    }
  }

  public class TestCaseBad
  {
    public string Name;
    public int Priority;
    public string Status;
  }

  public class Program
  {
    public static void Main()
    {
      // 1.1. Class basics and benefits

      string username = "qa_user";
      string password = "secure_password";
      Console.WriteLine($"Executing login with user: {username}");
      Console.WriteLine($"Test 1 Passed: {username == "qa_user" && password == "secure_password"}");

      string username2 = "wrong_user";
      string password2 = "wrong_password";
      Console.WriteLine($"Executing login with user: {username2}");
      Console.WriteLine($"Test 2 Passed: {username2 == "qa_user" && password2 == "secure!23"}");

      // Problems:
      // - Code repetition
      // - Hard to maintain (what if you change login logic?)
      // - No clear structure (data + behavior are not encapsulated what can lead to errors)

      var test1 = new LoginTest
      {
        Username = "qa_user",
        Password = "secure_password"
      };

      Console.WriteLine($"Test 1 Passed: {test1.Execute()}");

      var test2 = new LoginTest
      {
        Username = "wrong_user",
        Password = "wrong_password"
      };

      Console.WriteLine($"Test 2 Passed: {test2.Execute()}");
      
      // Each object (test1, test2) is a different execution of the same blueprint.

      // - Reusability: Once defined, you can reuse the class in different parts of your code.
      // - Abstraction: Hide unnecessary details, show only what matters.
      // - Maintainability: Code is easier to manage and update.
      // - Scalability: Easy to extend and adapt to future needs.
      // - Encapsulation: Bundle data & behavior together according to a context.

      // 1.2. Constructors

      // Creating objects using the constructor

      var test3 = new TestCaseBad();
      Console.WriteLine(test3.Status); // Null -> invalid status

      // Problems
      // - Without a constructor, you risk leaving objects in an invalid state (null or default values)
      // - This makes tests unreliable, just like running an automation test without proper setup.
    }
  }
}