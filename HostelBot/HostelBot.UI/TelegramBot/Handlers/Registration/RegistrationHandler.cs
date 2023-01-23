using HostelBot.Ui.TelegramBot.Commands;
using HostelBot.Ui.TelegramBot.Handlers.Filling;
using Telegram.Bot.Types;

namespace HostelBot.Ui.TelegramBot.Handlers.Registration;

internal static class RegistrationHandler
{
    public static async Task HandleRegistration(Update update, long chatId)
    {
        await FillingHandler.Handle(update, chatId, BaseCommands.StartCommand.GetFillable(chatId), true);
    }
}