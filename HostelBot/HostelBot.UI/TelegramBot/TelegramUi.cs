using HostelBot.App;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HostelBot.Ui.TelegramBot;

public class TelegramUi : IUi
{
    public TelegramUi(IApplication application)
    {
        Commands.SetStartCommand(application.GetStartCommand());
    }

    public void Run()
    {
        const string token = "5891142143:AAGPdh2b3te8nyC4GbyTLu4wYTDIK8czh58";
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, Error);
        
        Console.ReadLine();
    }

    private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message?.Chat.Id != null)
            Commands.AddUser(update.Message.Chat.Id);
        
        if (update.Type == UpdateType.CallbackQuery)
        {
            await Processor.HandleCallbackQuery(botClient, cancellationToken, update.CallbackQuery!);
            return;
        }
        
        if (!Commands.IsUserRegistered(update.Message.Chat.Id)
            && update.Message.Text != "/start" 
            && !FillingProgress.IsUserCurrentlyFilling(update.Message.Chat.Id))
        {
            await botClient.SendTextMessageAsync(update.Message!.Chat.Id, "Зарегистрируйтесь /start",
                cancellationToken: cancellationToken);
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

            if (Commands.Contains(text, chatId))
            {
                await Processor.HandleCommand(botClient, cancellationToken, Commands.Get(text, chatId), chatId);
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