using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot;

internal static class Processor
{
    public static async Task Start(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        
        var replyKeyboardMarkup = new ReplyKeyboardMarkup(new []
        {
            new KeyboardButton[] { KeyboardButtons.Start.Info, KeyboardButtons.Start.Service },
            new KeyboardButton[] { KeyboardButtons.Start.Question, KeyboardButtons.Start.Report }
        })
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false
        };
        
        await botClient.SendTextMessageAsync(message.Chat.Id, 
            "Welcome!", 
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    public static async Task Info(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(update.Message.Chat.Id,
            "Information about this bot will be displayed here",
            cancellationToken: cancellationToken);
    }
    
    public static async Task Service(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var replyKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(KeyboardButtons.Services.Electrician),
                InlineKeyboardButton.WithCallbackData(KeyboardButtons.Services.Janitor),
            },
            new[] { InlineKeyboardButton.WithCallbackData(KeyboardButtons.Services.Santekhnik) }
        });

        await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
            "Кого вы хотите вызвать?",
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }
    
    public static async Task Report(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        
    }
    
    public static async Task Question(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        
    }

    public static async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
            $"Вызываем сервис: {callbackQuery.Data}");
    }
}