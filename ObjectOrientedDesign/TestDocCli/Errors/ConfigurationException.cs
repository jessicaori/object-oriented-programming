namespace TestDocCli.Errors;

public sealed class ConfigurationException(string message)
    : KnownUserErrorException(message, ErrorCode.Configuration)
{
}
