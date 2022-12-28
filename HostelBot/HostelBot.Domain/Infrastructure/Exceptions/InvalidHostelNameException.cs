namespace HostelBot.Domain.Infrastructure.Exceptions;

public class InvalidHostelNameException : Exception
{
    public InvalidHostelNameException()
    {
    }

    public InvalidHostelNameException(Exception innerException)
        : base("", innerException)
    {
    }
}