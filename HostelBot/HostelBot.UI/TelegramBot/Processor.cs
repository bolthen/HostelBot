using HostelBot.App;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot;

internal static class Processor
{
    public static async Task Start(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, 
        CommandsHelper commandsHelper)
    {
        // var replyKeyboardMarkup = new ReplyKeyboardMarkup(new []
        // {
        //     new KeyboardButton[] { KeyboardButtons.Start.Info, KeyboardButtons.Start.Service },
        //     new KeyboardButton[] { KeyboardButtons.Start.Question, KeyboardButtons.Start.Report }
        // })
        var buttons = commandsHelper.Names.Select(x => new [] { new KeyboardButton(x) });
        
        var replyKeyboardMarkup = new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false
        };
        
        await botClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Welcome!", 
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }
    
    public static async Task HandleICommand(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, ICommand command)
    {
        IInteractionScenario scenario;
        try
        {
            scenario = command.GetScenario();
        }
        catch (NotImplementedException)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat.Id,
                $"Scenario for '{command.GetType().Name}' not implemented",
                cancellationToken: cancellationToken);
            return;
        }
        catch (Exception e)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat.Id,
                e.ToString(),
                cancellationToken: cancellationToken);
            return;
        }
        
        
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

    public static async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
            $"Вызываем сервис: {callbackQuery.Data}");
    }
}