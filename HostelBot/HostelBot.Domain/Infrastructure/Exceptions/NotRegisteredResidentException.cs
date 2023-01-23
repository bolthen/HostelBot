namespace HostelBot.Domain.Infrastructure.Exceptions;

public class NotRegisteredResidentException : Exception
{
    public NotRegisteredResidentException()
    {
    }

    public NotRegisteredResidentException(Exception innerException)
        : base("", innerException)
    {
    }
}