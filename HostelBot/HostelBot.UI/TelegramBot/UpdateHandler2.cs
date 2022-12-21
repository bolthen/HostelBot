using HostelBot.App;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;
using HostelBot.Ui.TelegramBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot;

internal static class BotClientHolder
{
    public static ITelegramBotClient BotClient;
}


internal static class UpdateHandler2
{
    public static async Task Handle(Update update)
    {
        var chatIdNullable = update.Message?.Chat.Id ?? update.CallbackQuery?.Message?.Chat.Id;
        if (!chatIdNullable.HasValue)
            return;

        var chatId = chatIdNullable.Value;
        switch (Registrator.GetRegistrationStatus(chatId))
        {
            case Registrator.RegistrationStatus.Registered:
                await CommonHandler.Handle(update, chatId);
                return;
            case Registrator.RegistrationStatus.NotRegistered:
                await RegistrationHandler.HandleRegistration(update, chatId);
                return;
            case Registrator.RegistrationStatus.WaitingApproval:
                await SharedHandlers.WaitVerification(chatId);
                return;
            case Registrator.RegistrationStatus.Unknown:
                Console.WriteLine("Unknown");
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

internal static class CommonHandler
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
            await FillingHandler.Handle(update, chatId, FillingProgress.GetProgress(chatId).fillable);
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

        await SharedHandlers.SendMessage("Unknown Common command", chatId);
    }
    
    private static async Task HandleCallbackQuery(Update update)
    {
        var callbackQuery = update.CallbackQuery!;
        var chatId = callbackQuery.Message!.Chat.Id;
        var messageId = callbackQuery.Message.MessageId;
        
        if (CallbackCommands.Contains(callbackQuery.Data!))
        {
            var command = CallbackCommands.Get(callbackQuery.Data!);

            await CommandHandler.Handle(command, chatId, update);

            await BotClientHolder.BotClient.EditMessageTextAsync(chatId, messageId,
                $"Вы выбрали: {command.Name}", 
                replyMarkup: new InlineKeyboardMarkup(Enumerable.Empty<InlineKeyboardButton>()));
            
            return;
        }

        await SharedHandlers.SendMessage($"Unknown Data: {callbackQuery.Data}", chatId);
    }
}


internal static class CommandHandler
{
    public static async Task Handle(Command command, long chatId, Update update)
    {
        var fillable = command.GetFillable(chatId);
        if (fillable != null)
        {
            await FillingHandler.Handle(update, chatId, fillable);
            return;
        }

        var subcommands = command.GetSubcommands(chatId);
        if (subcommands.Count == 0)
        {
            await SharedHandlers.SendMessage($"No subcommands for '{command.Name}' Command", chatId);
            return;
        }
        
        CallbackCommands.AddCommands(subcommands);

        var buttons = subcommands.Select(x => InlineKeyboardButton.WithCallbackData(x.Name));
        var replyKeyboardMarkup = new InlineKeyboardMarkup(buttons);

        await SharedHandlers.SendMessage(command.Name, chatId, replyMarkup: replyKeyboardMarkup);
    }
}


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

        await SendMessage("Авторизация прошла успешно.", chatId, replyMarkup: replyKeyboardMarkup);
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

internal static class FillingHandler
{
    public static async Task Handle(Update update, long chatId, IFillable fillable, bool isVerification = false)
    {
        if (FillingProgress.IsUserCurrentlyFilling(chatId))
        {
            await HandleProgress(update.Message!);
            return;
        }
        
        await HandleFillable(fillable, chatId, isVerification);
    }
    
    private static async Task HandleProgress(Message message)
    {
        var chatId = message.Chat.Id;
        var text = message.Text!;

        var progress = FillingProgress.GetProgress(chatId);

        if (!progress.TryValidateRegex(text, out var errorMessage))
        {
            await SharedHandlers.SendMessage(errorMessage!, chatId);
            return;
        }

        progress.SaveResponse(text);

        if (!progress.Completed)
        {
            await SharedHandlers.SendMessage(progress.GetNextQuestion(), chatId);
            return;
        }

        await SharedHandlers.SendMessage(progress.Answers.ToJsonFormat(), chatId);

        if (progress.IsVerification)
        {
            await SharedHandlers.WaitVerification(chatId);
        }

        FillingProgress.FinishFilling(chatId);
    }

    private static async Task HandleFillable(IFillable fillable, long chatId, bool isVerification = false)
    {
        var progress = new FillingProgress(fillable, chatId, isVerification);

        await SharedHandlers.SendMessage(progress.GetNextQuestion(), chatId);
    }
}

internal static class RegistrationHandler
{
    public static async Task HandleRegistration(Update update, long chatId)
    {
        await FillingHandler.Handle(update, chatId, BaseCommands.StartCommand.GetFillable(chatId), true);
    }
}

public static class BaseCommands
{
    public static Command StartCommand { get; private set; }

    public static void SetStartCommand(Command command)
    {
        StartCommand = command;
    }
    

    public static IEnumerable<Command> GetBaseCommands(long chatId)
    {
        return StartCommand.GetSubcommands(chatId);
    }

    public static IEnumerable<string> GetBaseCommandsNames(long chatId)
    {
        return GetBaseCommands(chatId).Select(x => x.Name);
    }
    
    public static bool Contains(string commandName, long chatId)
    {
        return GetBaseCommandsNames(chatId).Contains(commandName);
    }
    
    public static Command Get(string commandName, long chatId)
    {
        return GetBaseCommands(chatId).First(x => x.Name == commandName);
    }
}

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

    public enum RegistrationStatus
    {
        NotRegistered, WaitingApproval, Registered, Unknown
    }
}