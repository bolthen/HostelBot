﻿using HostelBot.App;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HostelBot.Ui.TelegramBot;

public class TelegramUi : IUi
{
    public TelegramUi(IApplication application)
    {
        BaseCommands.SetStartCommand(application.GetStartCommand());
        
        application.GetAppealChangesManager().AddChangesHandler(UpdateHandler.HandleAppeal);
        application.GetResidentChangesManager().AddChangesHandler(UpdateHandler.HandleResident);
    }

    public void Run()
    {
        const string token = "5891142143:AAGPdh2b3te8nyC4GbyTLu4wYTDIK8czh58";
        
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, Error, receiverOptions: new ReceiverOptions
        {
            AllowedUpdates = new [] { UpdateType.Message, UpdateType.CallbackQuery }
        });

        UpdateHandler.BotClient = client;
        
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