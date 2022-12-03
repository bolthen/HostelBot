using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure;

public class AppealManager : Manager<Appeal>
{
    protected override void Handle(Appeal value)
    {
        // Сохранение в БД
    }
}