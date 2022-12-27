using HostelBot.Ui.TelegramBot.Registration;
using Telegram.Bot.Types;

namespace HostelBot.Ui.TelegramBot.Handlers;

internal static class UpdateHandler
{
    public static async Task Handle(Update update)
    {
        var chatIdNullable = update.Message?.Chat.Id ?? update.CallbackQuery?.Message?.Chat.Id;
        if (!chatIdNullable.HasValue)
            return;

        var chatId = chatIdNullable.Value;
        switch (Registrator.GetRegistrationStatus(chatId))
        {
            case RegistrationStatus.Registered:
                await RegisteredHandler.Handle(update, chatId);
                return;
            case RegistrationStatus.NotRegistered:
                await RegistrationHandler.HandleRegistration(update, chatId);
                return;
            case RegistrationStatus.WaitingApproval:
                await SharedHandlers.WaitVerification(chatId);
                return;
            case RegistrationStatus.Unknown:
                Console.WriteLine("Unknown");
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}