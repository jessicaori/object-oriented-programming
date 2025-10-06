namespace TestDocCli.Errors;

public class ValidationException(string message)
    : KnownUserErrorException(message, ErrorCode.Validation)
{
}
