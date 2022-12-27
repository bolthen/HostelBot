using HostelBot.Domain.Domain;
using HostelBot.Ui.TelegramBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot.Handlers;

internal static class SharedHandlers
{
    public static async Task SendMessage(string message, long chatId, IReplyMarkup? replyMarkup = null)
    {
        await BotClientHolder.BotClient.SendTextMessageAsync(chatId, message, replyMarkup: replyMarkup);
    }
    
    public static async Task WaitVerification(long chatId)
    {
        await SendMessage("Подождите пока вас примут.", chatId);
    }

    public static async Task AuthorizeUser(long chatId)
    {
        var buttons = BaseCommands.GetBaseCommandsNames(chatId)
            .Select(name => new[] { new KeyboardButton(name) });
        var replyKeyboardMarkup = new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false
        };

        await SendMessage("Добро пожаловать.", chatId, replyMarkup: replyKeyboardMarkup);
    }
    
    
    public static async void NotifyResidentAccepted(Resident resident)
    {
        LocalUserRepo.RegisterUser(resident.Id);
        await AuthorizeUser(resident.Id);
    }
    
    public static async void NotifyAppealResponseReceived(Appeal appeal)
    {
        await SendMessage("Ответ на ваше обращение:\n" + appeal.Answer, appeal.Resident.Id);
    }
}