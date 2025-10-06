using Moq;
using TestDocCli.AppCore;
using TestDocCli.InputOutput;
using TestDocCli.Model;
using Xunit;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TestDocCli.Tests.AppCore;

public class PromptFlowTests
{
    private readonly Mock<IConsole> consoleMock = new(MockBehavior.Strict);
    private readonly Mock<IInputReader> readerMock = new(MockBehavior.Strict);

    public PromptFlowTests()
    {
        consoleMock.Setup(x => x.WriteLine(It.IsAny<string>())).Verifiable();
    }

    [Fact]
    public void CollectTestDocument_WithoutMoq_ShouldCaptureConsole()
    {
        var console = new FakeConsole();
        var reader = new FakeInputReader(
            new Dictionary<string, string>
            {
                { "Title", "Checkout" },
                { "Description", "User can checkout" },
                { "Expected Result", "Order completed" },
                { "Actual Result", "Order un-completed" }
            },
            new List<string> { "Add to cart", "Pay" }
        );
        var promptFlow = new PromptFlow(reader, console);

        TestDocument result = promptFlow.CollectTestDocument();

        Assert.Equal("Checkout", result.Title);
        Assert.Equal(2, result.Steps.Count);
        Assert.Equal("Add to cart", result.Steps[0]);
        Assert.Equal("Pay", result.Steps[1]);
        Assert.Equal("Order completed", result.ExpectedResult);
        Assert.Equal("Order un-completed", result.ActualResult);
        Assert.Contains("Test Documentation Generator", console.JoinedOutput);
    }

    [Fact]
    public void CollectTestDocument_WithMoq_ShouldCaptureConsole()
    {
        readerMock.Setup(x => x.ReadRequired("Title")).Returns("Checkout");
        readerMock.Setup(x => x.ReadRequired("Description")).Returns("User can checkout");
        readerMock.SetupSequence(x => x.ReadLine(It.Is<string>(s => s.StartsWith("Step "))))
            .Returns("Add to cart")
            .Returns("Pay")
            .Returns(string.Empty);
        readerMock.Setup(x => x.ReadRequired("Expected Result")).Returns("Order completed");
        readerMock.Setup(x => x.ReadRequired("Actual Result")).Returns("Order un-completed");

        var promptFlow = new PromptFlow(readerMock.Object, consoleMock.Object);

        TestDocument result = promptFlow.CollectTestDocument();

        Assert.Equal("Checkout", result.Title);
        Assert.Equal(2, result.Steps.Count);
        Assert.Equal("Add to cart", result.Steps[0]);
        Assert.Equal("Pay", result.Steps[1]);
        Assert.Equal("Order completed", result.ExpectedResult);
        Assert.Equal("Order un-completed", result.ActualResult);

        consoleMock.VerifyAll();
        readerMock.VerifyAll();
    }

    [Fact]
    public void CollectTestDocument_NoSteps_HandlesGracefully()
    {
        readerMock.Setup(x => x.ReadRequired("Title")).Returns("Login");
        readerMock.Setup(x => x.ReadRequired("Description")).Returns("User logs in");
        readerMock.SetupSequence(x => x.ReadLine(It.Is<string>(s => s.StartsWith("Step "))))
            .Returns(string.Empty);
        readerMock.Setup(x => x.ReadRequired("Expected Result")).Returns("Dashboard visible");
        readerMock.Setup(x => x.ReadRequired("Actual Result")).Returns("Dashboard visible");

        var promptFlow = new PromptFlow(readerMock.Object, consoleMock.Object);

        TestDocument result = promptFlow.CollectTestDocument();

        Assert.Equal("Login", result.Title);
        Assert.Empty(result.Steps);
        Assert.Equal("Dashboard visible", result.ExpectedResult);
        Assert.Equal("Dashboard visible", result.ActualResult);

        consoleMock.VerifyAll();
        readerMock.VerifyAll();
    }

    [Fact]
    public void CollectTestDocument_MissingLabel_ThrowsException()
    {
        var reader = new FakeInputReader(
            new Dictionary<string, string>
            {
                { "Title", "Profile Update" },
                // Missing "Description"
                { "Expected Result", "Profile saved" },
                { "Actual Result", "Profile saved" }
            },
            new List<string>()
        );
        var console = new FakeConsole();
        var promptFlow = new PromptFlow(reader, console);

        Assert.Throws<InvalidOperationException>(() => promptFlow.CollectTestDocument());
    }

    private sealed class FakeConsole : IConsole
    {
        private readonly List<string> _lines = new();
        public string JoinedOutput => string.Join('\n', _lines);

        public string ReadLine()
        {
            return string.Empty;
        }

        public void Write(string message)
        {
            _lines.Add(message);
        }

        public void WriteLine(string message)
        {
            _lines.Add(message);
        }
    }

    private sealed class FakeInputReader : IInputReader
    {
        private readonly Dictionary<string, string> _answerByLabel;
        private readonly List<string> _steps;

        public FakeInputReader(Dictionary<string, string> answerByLabel, List<string> steps)
        {
            _answerByLabel = answerByLabel;
            _steps = steps;
        }

        public string ReadLine(string prompt)
        {
            if (_steps.Count == 0)
            {
                return string.Empty;
            }

            var first = _steps.First();
            _steps.RemoveAt(0);
            return first;
        }

        public string ReadRequired(string label)
        {
            if (_answerByLabel.TryGetValue(label, out string? value))
            {
                return value;
            }

            throw new InvalidOperationException($"No answer for label: {label}");
        }
    }
}
