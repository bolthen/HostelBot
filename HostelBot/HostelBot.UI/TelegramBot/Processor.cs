using HostelBot.App;
using HostelBot.Domain.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot;

internal static class Processor
{
    public static async Task Start(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // var replyKeyboardMarkup = new ReplyKeyboardMarkup(new []
        // {
        //     new KeyboardButton[] { KeyboardButtons.Start.Info, KeyboardButtons.Start.Service },
        //     new KeyboardButton[] { KeyboardButtons.Start.Question, KeyboardButtons.Start.Report }
        // })

        await HandleCommand(botClient, cancellationToken, Commands.StartCommand, update.Message!.Chat.Id);
        await botClient.SendTextMessageAsync(update.Message!.Chat.Id, "Верифицируем вас, ждите...",
            cancellationToken: cancellationToken);
        
        var buttons = Commands.Names.Select(x => new [] { new KeyboardButton(x) });
        var replyKeyboardMarkup = new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false
        };
        
        await botClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Верификация прошла успешно", 
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }
    
    public static async Task HandleCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, 
        Command command, long chatId)
    {
        var fillable = command.GetFillable(chatId);
        if (fillable != null)
        {
            await HandleFillable(botClient, cancellationToken, fillable, chatId);
            return;
        }

        var subcommands = command.GetSubcommands(chatId);
        if (subcommands.Count == 0)
        {
            await botClient.SendTextMessageAsync(chatId, $"No info for {command.Name} Command",
                cancellationToken: cancellationToken);
            return;
        }
        
        Commands.AddCommands(subcommands);
        
        var buttons = subcommands.Select(x => InlineKeyboardButton.WithCallbackData(x.Name));
        var replyKeyboardMarkup = new InlineKeyboardMarkup(buttons);
        
        await botClient.SendTextMessageAsync(chatId, 
            command.Name,
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    private static async Task HandleFillable(ITelegramBotClient botClient, CancellationToken cancellationToken, 
        IFillable fillable, long chatId)
    {
        var progress = new FillingProgress(fillable, chatId);

        await botClient.SendTextMessageAsync(chatId, progress.GetNextQuestion(),
            cancellationToken: cancellationToken);
    }

    public static async Task HandleProgress(ITelegramBotClient botClient, CancellationToken cancellationToken, 
        Message message)
    {
        var chatId = message.Chat.Id;
        var text = message.Text!;

        var progress = FillingProgress.GetProgress(chatId);
        
        if (!progress.TryValidateRegex(text, out var errorMessage))
        {
            await botClient.SendTextMessageAsync(chatId, errorMessage!, cancellationToken: cancellationToken);
            return;
        }
        
        progress.SaveResponse(text);
                
        if (progress.Completed)
        {
            await botClient.SendTextMessageAsync(chatId, progress.Result.ToJsonFormat(), cancellationToken: cancellationToken);
            FillingProgress.FinishFilling(chatId);
            return;
        }
                
        await botClient.SendTextMessageAsync(chatId, progress.GetNextQuestion(), cancellationToken: cancellationToken);
    }

    public static async Task HandleCallbackQuery(ITelegramBotClient botClient, CancellationToken cancellationToken, 
        CallbackQuery callbackQuery)
    {
        if (Commands.Contains(callbackQuery.Data!))
        {
            await HandleCommand(botClient, cancellationToken, Commands.Get(callbackQuery.Data!), 
                callbackQuery.Message!.Chat.Id);
            return;
        }
        
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Unknown Data: {callbackQuery.Data}",
            cancellationToken: cancellationToken);
    }
}