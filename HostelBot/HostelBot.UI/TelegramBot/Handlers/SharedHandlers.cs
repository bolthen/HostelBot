using HostelBot.Domain.Domain;
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
        await SendMessage("Добро пожаловать.", chatId, BaseButtonsMarkup.Get(chatId));
    }
    
    
    public static async void NotifyResidentAccepted(Resident resident)
    {
        await AuthorizeUser(resident.Id);
    }
    
    public static async void NotifyAppealResponseReceived(Appeal appeal)
    {
        await SendMessage("Ответ на ваше обращение:\n" + appeal.Answer, appeal.Resident.Id);
    }
}