
// BAD: Using just variables without a class
int testId = 1;
string testName = "Login with valid credentials";
IEnumerable<string> testSteps =
[
  "1. Open App",
  "2. Enter username",
  "3. Enter password"
];
Console.WriteLine($"Executing {testName}");

// Problems:
// - Code duplication
// - No structure (everything is loose variables)
// - Hard to maintain
