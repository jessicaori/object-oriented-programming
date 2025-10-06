using TestDocCli.AppCore;
using TestDocCli.Errors;
using TestDocCli.Formatters;
using Xunit;
using System;
using System.Collections.Generic;

namespace TestDocCli.Tests.AppCore;

public class OutputFormatterFactoryTests
{
    public static IEnumerable<object[]> SupportedFormats => new List<object[]>
    {
        new object[] { "", typeof(PlainConsoleFormatter) },
        new object[] { "console", typeof(PlainConsoleFormatter) },
        new object[] { "text", typeof(PlainConsoleFormatter) },
        new object[] { "html", typeof(HtmlFormatter) },
        new object[] { "md", typeof(MarkdownFormatter) },
        new object[] { "markdown", typeof(MarkdownFormatter) },
    };

    [Theory]
    [MemberData(nameof(SupportedFormats))]
    public void Create_SupportedFormats_ReturnsCorrectFormatter(string input, Type expectedType)
    {
        var factory = new OutputFormatterFactory();

        IOutputFormatter formatter = factory.Create(input);

        Assert.IsType(expectedType, formatter);
    }

    [Fact]
    public void Create_UnknownFormat_ThrowsConfigurationException()
    {
        var factory = new OutputFormatterFactory();

        Assert.Throws<ConfigurationException>(() => factory.Create("pdf"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Create_NullOrWhitespaceInput_ReturnsPlainConsoleFormatter(string input)
    {
        var factory = new OutputFormatterFactory();

        var formatter = factory.Create(input);

        Assert.IsType<PlainConsoleFormatter>(formatter);
    }

    [Theory]
    [InlineData("HTML", typeof(HtmlFormatter))]
    [InlineData("Md", typeof(MarkdownFormatter))]
    [InlineData("MARKDOWN", typeof(MarkdownFormatter))]
    [InlineData("TEXT", typeof(PlainConsoleFormatter))]
    public void Create_FormatInput_IsCaseInsensitive(string input, Type expectedType)
    {
        var factory = new OutputFormatterFactory();

        var formatter = factory.Create(input);

        Assert.IsType(expectedType, formatter);
    }

    [Theory]
    [InlineData(" html ", typeof(HtmlFormatter))]
    [InlineData("  markdown", typeof(MarkdownFormatter))]
    [InlineData("text  ", typeof(PlainConsoleFormatter))]
    public void Create_InputWithExtraWhitespace_ReturnsCorrectFormatter(string input, Type expectedType)
    {
        var factory = new OutputFormatterFactory();

        var formatter = factory.Create(input);

        Assert.IsType(expectedType, formatter);
    }

    [Fact]
    public void Create_CalledMultipleTimes_ReturnsNewInstances()
    {
        var factory = new OutputFormatterFactory();

        var first = factory.Create("html");
        var second = factory.Create("html");

        Assert.NotSame(first, second);
    }
}
