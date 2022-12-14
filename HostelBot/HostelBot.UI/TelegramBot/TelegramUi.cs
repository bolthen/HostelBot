using HostelBot.App;
using HostelBot.Domain.Infrastructure.Repository;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HostelBot.Ui.TelegramBot;

public class TelegramUi : IUi
{
    public TelegramUi(IApplication application)
    {
        Commands.AddCommands(application.GetBaseCommands());
    }

    public void Run()
    {
        const string token = "5818008930:AAFyER7tuVgbwWyLgclM7BYMvBdnn3GLMjg";
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, Error);
        
        Console.ReadLine();
    }

    private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await Processor.HandleCallbackQuery(botClient, cancellationToken, update.CallbackQuery!);
            return;
        }

        if (update.Type == UpdateType.Message && update.Message?.Text != null)
        {
            var message = update.Message;
            var text = message.Text!;
            var chatId = message.Chat.Id;
            
            if (text == "/start")
            {
                await Processor.Start(botClient, update, cancellationToken);
                return;
            }

            if (FillingProgress.IsUserCurrentlyFilling(chatId))
            {
                await Processor.HandleProgress(botClient, cancellationToken, message);
                return;
            }

            if (Commands.Contains(text))
            {
                await Processor.HandleCommand(botClient, cancellationToken, Commands.Get(text), chatId);
                return;
            }
        }
        
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"UpdateType: {update.Type}", 
            cancellationToken: cancellationToken);
    }

    private Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}