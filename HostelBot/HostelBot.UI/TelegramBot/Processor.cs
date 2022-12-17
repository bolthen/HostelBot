using HostelBot.App;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot;

internal static class Processor
{
    public static async Task Start(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chatId = update.Message!.Chat.Id;
        
        List<Command> baseCommands;
        try
        {
            baseCommands = Commands.StartCommand.GetSubcommands(chatId);
        }
        catch (NotRegisteredResidentException)
        {
            var fillable = Commands.StartCommand.GetFillable(chatId);
            await HandleFillable(botClient, cancellationToken, fillable, chatId, Commands.StartCommand);
            return;
        }
        catch (Exception e)
        {
            await botClient.SendTextMessageAsync(chatId, $"Unknown Exception: {e}", cancellationToken: cancellationToken);
            return;
        }
        
        Commands.AddCommands(baseCommands, chatId);
        Commands.RegisterUser(chatId);

        var buttons = baseCommands.Select(x => new [] { new KeyboardButton(x.Name) });
        var replyKeyboardMarkup = new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false
        };
        
        await botClient.SendTextMessageAsync(chatId, 
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
            await HandleFillable(botClient, cancellationToken, fillable, chatId, command);
            return;
        }

        var subcommands = command.GetSubcommands(chatId);
        if (subcommands.Count == 0)
        {
            await botClient.SendTextMessageAsync(chatId, $"No subcommands for '{command.Name}' Command",
                cancellationToken: cancellationToken);
            return;
        }
        
        Commands.AddCommands(subcommands, chatId);
        
        var buttons = subcommands.Select(x => InlineKeyboardButton.WithCallbackData(x.Name));
        var replyKeyboardMarkup = new InlineKeyboardMarkup(buttons);
        
        await botClient.SendTextMessageAsync(chatId, 
            command.Name,
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);
    }

    private static async Task HandleFillable(ITelegramBotClient botClient, CancellationToken cancellationToken, 
        IFillable fillable, long chatId, Command command)
    {
        var progress = new FillingProgress(fillable, chatId, command);

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
            
            if (progress.Command.GetType().Name == Commands.StartCommand.GetType().Name)
            {
                await WaitVerification(botClient, chatId, cancellationToken);
            }
            
            FillingProgress.FinishFilling(chatId);
            
            return;
        }
                
        await botClient.SendTextMessageAsync(chatId, progress.GetNextQuestion(), cancellationToken: cancellationToken);
    }

    private static async Task WaitVerification(ITelegramBotClient botClient, long chatId, 
        CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(chatId, 
            "Подождите пока вас верифицируют.\nЧтобы проверить статус верификации, нажмите /start.",
            cancellationToken: cancellationToken);
    }

    public static async Task HandleCallbackQuery(ITelegramBotClient botClient, CancellationToken cancellationToken, 
        CallbackQuery callbackQuery, long chatId)
    {
        if (Commands.Contains(callbackQuery.Data!, chatId))
        {
            await HandleCommand(botClient, cancellationToken, Commands.Get(callbackQuery.Data!, chatId), 
                callbackQuery.Message!.Chat.Id);
            return;
        }
        
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Unknown Data: {callbackQuery.Data}",
            cancellationToken: cancellationToken);
    }
}