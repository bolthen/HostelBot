using HostelBot.Ui.TelegramBot.Commands;
using HostelBot.Ui.TelegramBot.Handlers.Filling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot.Handlers;

internal static class RegisteredHandler
{
    public static async Task Handle(Update update, long chatId)
    {
        if (!LocalUserRepo.ContainsUser(chatId))
        {
            LocalUserRepo.RegisterUser(chatId);
            await SharedHandlers.AuthorizeUser(chatId);
            return;
        }

        if (FillingProgress.IsUserCurrentlyFilling(chatId))
        {
            await FillingHandler.Handle(update, chatId, FillingProgress.GetFillingProgress(chatId).fillable);
            return;
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            await HandleCallbackQuery(update);
            return;
        }

        var text = update.Message!.Text!;
        
        if (BaseCommands.Contains(text, chatId))
        {
            await CommandHandler.Handle(BaseCommands.Get(text, chatId), chatId, update);
            return;
        }

        await SharedHandlers.SendMessage($"Не понимаем вас: {text}", chatId);
    }
    
    private static async Task HandleCallbackQuery(Update update)
    {
        var callbackQuery = update.CallbackQuery!;
        var chatId = callbackQuery.Message!.Chat.Id;
        var messageId = callbackQuery.Message.MessageId;
        
        if (CallbackCommands.Contains(callbackQuery.Data!))
        {
            var command = CallbackCommands.Get(callbackQuery.Data!); 

            await BotClientHolder.BotClient.EditMessageTextAsync(chatId, messageId,
                $"Вы выбрали: {command.Name}", 
                replyMarkup: new InlineKeyboardMarkup(Enumerable.Empty<InlineKeyboardButton>()));

            await CommandHandler.Handle(command, chatId, update);
            
            return;
        }

        await SharedHandlers.SendMessage($"Unknown Data: {callbackQuery.Data}", chatId);
    }
}