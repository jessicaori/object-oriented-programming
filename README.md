# How to create a console application project

See the example below

## 1. Solution & projects
mkdir InvoiceSolution && cd InvoiceSolution

dotnet new sln -n InvoiceSolution

dotnet new console -n InvoiceApp

dotnet new xunit   -n InvoiceApp.Tests

## 2. Add projects to the solution

dotnet sln add InvoiceApp/InvoiceApp.csproj

dotnet sln add InvoiceApp.Tests/InvoiceApp.Tests.csproj

## 3. Test project references main project

dotnet add InvoiceApp.Tests reference InvoiceApp

## 4. Install dependencies
dotnet add InvoiceApp package Microsoft.Extensions.Hosting
dotnet add InvoiceApp package Microsoft.Extensions.DependencyInjection

## 5. Run the app (uses inline sample data)

dotnet run --project InvoiceApp

## 6. Run tests

dotnet test

# How to contribute to the project

If you know git you can use the following tutorial:

https://www.youtube.com/watch?v=YaToH3s_-nQ
