namespace TestDocCli.Errors;

public abstract class KnownUserErrorException : Exception
{
    public ErrorCode ExitCode { get; }

    protected KnownUserErrorException(string message, ErrorCode exitCode, Exception? inner = null)
        : base(message, inner)
    {
        ExitCode = exitCode;
    }
}
