using HostelBot.App;
using HostelBot.Ui.TelegramBot.Commands;
using HostelBot.Ui.TelegramBot.Handlers;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HostelBot.Ui.TelegramBot;

public class TelegramResidentUi : IResidentUi
{
    public TelegramResidentUi(IApplication application)
    {
        BaseCommands.SetStartCommand(application.GetStartCommand());
        
        application.GetAppealChangesHandler().AddChangesHandler(SharedHandlers.NotifyAppealResponseReceived);
        application.GetResidentChangesHandler().AddChangesHandler(SharedHandlers.NotifyResidentAccepted);
    }

    public void Run()
    {
        const string token = "5907576996:AAG4e3-Nwd30BiHI8O793YgkdXtpY0EqZoU";
        
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, Error, receiverOptions: new ReceiverOptions
        {
            AllowedUpdates = new [] { UpdateType.Message, UpdateType.CallbackQuery }
        });

        BotClientHolder.BotClient = client;
        
        Console.ReadLine();
    }

    private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await UpdateHandler.Handle(update);
    }

    private Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(exception);
        return Task.CompletedTask;
    }
}