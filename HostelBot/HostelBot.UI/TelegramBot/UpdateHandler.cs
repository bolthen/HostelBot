using HostelBot.App;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot;

internal static class UpdateHandler
{
    public static ITelegramBotClient BotClient;

    private static readonly BaseCommands BaseCommands = new();
    private static readonly CallbackCommands CallbackCommands = new();

    public static async Task Handle(Update update)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await HandleCallbackQuery(update.CallbackQuery!);
            return;
        }
        
        var chatId = update.Message!.Chat.Id;
        var text = update.Message.Text!;

        if (update.Type == UpdateType.Message)
        {
            if (!LocalUserRepo.ContainsUser(chatId))
                LocalUserRepo.AddUser(chatId);
            
            if (!LocalUserRepo.IsRegistered(chatId) && !FillingProgress.IsUserCurrentlyFilling(chatId))
            {   
                await Start(update);
                return;
            }

            if (FillingProgress.IsUserCurrentlyFilling(chatId))
            {
                await HandleProgress(update.Message);
                return;
            }

            if (BaseCommands.Contains(text))
            {
                await HandleCommand(BaseCommands.Get(text), chatId);
                return;
            }
            
            if (CallbackCommands.Contains(text))
            {
                await HandleCommand(CallbackCommands.Get(text), chatId);
                return;
            }
        }

        await SendMessage($"UpdateType: {update.Type}", chatId);
    }

    private static async Task CheckRegistration(long chatId)
    {
        List<Command> commands;
        try
        {
            commands = BaseCommands.StartCommand.GetSubcommands(chatId);
        }
        catch (NotRegisteredResidentException)
        {
            var fillable = BaseCommands.StartCommand.GetFillable(chatId);
            await HandleFillable(fillable!, chatId, BaseCommands.StartCommand);
            return;
        }
        catch (NotAcceptedResidentException)
        {
            await WaitVerification(chatId);
            return;
        }
        catch (Exception e)
        {
            await SendMessage($"Unknown Exception: {e}", chatId);
            return;
        }
        
        if (LocalUserRepo.IsRegistered(chatId))
            return;
        
        BaseCommands.AddCommands(commands);
        LocalUserRepo.RegisterUser(chatId);
    }

    private static async Task Start(Update update)
    {
        var chatId = update.Message!.Chat.Id;
        await CheckRegistration(chatId);
        if (LocalUserRepo.IsRegistered(chatId))
            await VerificationSuccessful(chatId);
    }

    private static async Task VerificationSuccessful(long chatId)
    {
        var buttons = BaseCommands.GetCommands().Select(x => new[] { new KeyboardButton(x.Name) });
        var replyKeyboardMarkup = new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false
        };

        await BotClient.SendTextMessageAsync(chatId, "Верификация прошла успешно.",
            replyMarkup: replyKeyboardMarkup);
    }

    private static async Task HandleCommand(Command command, long chatId)
    {
        var fillable = command.GetFillable(chatId);
        if (fillable != null)
        {
            await HandleFillable(fillable, chatId, command);
            return;
        }

        var subcommands = command.GetSubcommands(chatId);
        if (subcommands.Count == 0)
        {
            await SendMessage($"No subcommands for '{command.Name}' Command", chatId);
            return;
        }

        if (command.GetType().Name == BaseCommands.StartCommand.GetType().Name)
        {
            BaseCommands.AddCommands(subcommands);
            return;
        }
        
        CallbackCommands.AddCommands(subcommands);

        var buttons = subcommands.Select(x => InlineKeyboardButton.WithCallbackData(x.Name));
        var replyKeyboardMarkup = new InlineKeyboardMarkup(buttons);

        await BotClient.SendTextMessageAsync(chatId, command.Name, replyMarkup: replyKeyboardMarkup);
    }

    private static async Task HandleFillable(IFillable fillable, long chatId, Command command)
    {
        var progress = new FillingProgress(fillable, chatId, command);

        await SendMessage(progress.GetNextQuestion(), chatId);
    }

    private static async Task HandleProgress(Message message)
    {
        var chatId = message.Chat.Id;
        var text = message.Text!;

        var progress = FillingProgress.GetProgress(chatId);

        if (!progress.TryValidateRegex(text, out var errorMessage))
        {
            await SendMessage(errorMessage!, chatId);
            return;
        }

        progress.SaveResponse(text);

        if (!progress.Completed)
        {
            await SendMessage(progress.GetNextQuestion(), chatId);
            return;
        }

        await SendMessage(progress.Answers.ToJsonFormat(), chatId);

        if (progress.Command.GetType().Name == BaseCommands.StartCommand.GetType().Name)
        {
            await WaitVerification(chatId);
        }

        FillingProgress.FinishFilling(chatId);
    }

    private static async Task WaitVerification(long chatId)
    {
        await SendMessage("Подождите пока вас примут.", chatId);
    }

    private static async Task HandleCallbackQuery(CallbackQuery callbackQuery)
    {
        var chatId = callbackQuery.Message!.Chat.Id;
        
        if (CallbackCommands.Contains(callbackQuery.Data!))
        {
            await HandleCommand(CallbackCommands.Get(callbackQuery.Data!), chatId);
            return;
        }
        
        await SendMessage($"Unknown Data: {callbackQuery.Data}", chatId);
    }

    private static async Task SendMessage(string message, long chatId, ReplyMarkupBase? replyMarkup = null)
    {
        await BotClient.SendTextMessageAsync(chatId, message, replyMarkup: replyMarkup);
    }
    
    public static async void HandleResident(Resident resident)
    {
        await VerificationSuccessful(resident.Id);
    }
    
    public static async void HandleAppeal(Appeal appeal)
    {
        await SendMessage("Ответ на ваше обращение:\n" + appeal.Answer, appeal.Resident.Id);
    }
}