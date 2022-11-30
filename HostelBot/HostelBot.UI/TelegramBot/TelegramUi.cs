using HostelBot.App;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HostelBot.Ui.TelegramBot;

public class TelegramUi : IUi
{
    public TelegramUi(IApplication application)
    {
        commandsHelper = new CommandsHelper(application.GetBaseCommands());
    }

    public void Run()
    {
        const string token = "5818008930:AAFyER7tuVgbwWyLgclM7BYMvBdnn3GLMjg";
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, Error);
        
        Console.ReadLine();
    }

    private readonly CommandsHelper commandsHelper;

    private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await Processor.HandleCallbackQuery(botClient, update.CallbackQuery!);
            return;
        }

        if (update.Type == UpdateType.Message && update.Message?.Text != null)
        {
            var message = update.Message;
            var text = message.Text!;
            var chatId = message.Chat.Id;
            
            if (text == "/start")
            {
                await Processor.Start(botClient, update, cancellationToken, commandsHelper);
                return;
            }

            if (commandsHelper.NameToCommand.ContainsKey(text))
            {
                await Processor.HandleBaseICommands(botClient, update, cancellationToken, 
                    commandsHelper.NameToCommand[text], commandsHelper);
                return;
            }

            if (commandsHelper.ChatIdToFillingProgress.ContainsKey(chatId))
            {
                var progress = commandsHelper.ChatIdToFillingProgress[chatId];
                progress.SaveResponse(text);
                
                if (progress.Completed)
                {
                    await botClient.SendTextMessageAsync(chatId, "Completed", cancellationToken: cancellationToken);
                    commandsHelper.ChatIdToFillingProgress.Remove(chatId);
                    return;
                }
                
                await botClient.SendTextMessageAsync(chatId, progress.GetNextQuestion(), cancellationToken: cancellationToken); 
                
                return;
            }
        }
        
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"UpdateType: {update.Type}", 
            cancellationToken: cancellationToken);
    }

    private Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}