namespace TreeApp.Domain.Exceptions;
public class SecureException : Exception
{
    public SecureException(string message) : base(message) { }
}