namespace HostelBot.Domain.Infrastructure.Exceptions;

public class HostelException : Exception
{
    public HostelException()
    {
    }

    public HostelException(Exception innerException)
        : base("", innerException)
    {
    }
}