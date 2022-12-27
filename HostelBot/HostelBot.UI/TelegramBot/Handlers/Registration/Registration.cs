using HostelBot.Domain.Infrastructure.Exceptions;
using HostelBot.Ui.TelegramBot.Commands;

namespace HostelBot.Ui.TelegramBot.Registration;

internal static class Registrator
{
    public static RegistrationStatus GetRegistrationStatus(long chatId)
    {
        try
        {
            var _ = BaseCommands.StartCommand.GetSubcommands(chatId);
            return RegistrationStatus.Registered;
        }
        catch (NotRegisteredResidentException)
        {
            return RegistrationStatus.NotRegistered;
        }
        catch (NotAcceptedResidentException)
        {
            return RegistrationStatus.WaitingApproval;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RegistrationStatus.Unknown;
        }
    }
}