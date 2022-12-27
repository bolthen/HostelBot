using HostelBot.Domain.Infrastructure;
using Telegram.Bot.Types;

namespace HostelBot.Ui.TelegramBot.Handlers.Filling;

internal static class FillingHandler
{
    public static async Task Handle(Update update, long chatId, IFillable fillable, bool isRegistration = false)
    {
        if (FillingProgress.IsUserCurrentlyFilling(chatId))
        {
            await HandleProgress(update.Message!);
            return;
        }
        
        await StartFilling(fillable, chatId, isRegistration);
    }
    
    private static async Task HandleProgress(Message message)
    {
        var chatId = message.Chat.Id;
        var text = message.Text!;

        var progress = FillingProgress.GetFillingProgress(chatId);
        var result = progress.HandleResponse(text.Trim());
        
        switch (result.progressStatus)
        {
            case FillingProgress.CurrentProgressStatus.WrittenDown:
                await SharedHandlers.SendMessage(progress.GetNextQuestion(), chatId);
                return;
            case FillingProgress.CurrentProgressStatus.RegexFailed:
                await SharedHandlers.SendMessage(result.errorMessage!, chatId);
                return;
            case FillingProgress.CurrentProgressStatus.Completed:
                var answers = progress.Answers;
                var fillable = progress.fillable;
                FillingProgress.FinishFilling(chatId);
                
                fillable.FillClass(answers);
                
                await SharedHandlers.SendMessage(answers.ToJsonFormat(), chatId);

                if (progress.IsRegistration)
                    await SharedHandlers.WaitVerification(chatId);
                
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }

        // if (!progress.TryValidateRegex(text, out var errorMessage))
        // {
        //     await SharedHandlers.SendMessage(errorMessage!, chatId);
        //     return;
        // }
        //
        // progress.SaveResponse(text);
        //
        // if (!progress.Completed)
        // {
        //     await SharedHandlers.SendMessage(progress.GetNextQuestion(), chatId);
        //     return;
        // }
        //
        // await SharedHandlers.SendMessage(progress.Answers.ToJsonFormat(), chatId);

        // if (progress.IsRegistration)
        // {
        //     await SharedHandlers.WaitVerification(chatId);
        // }

        // FillingProgress.FinishFilling(chatId);
    }

    private static async Task StartFilling(IFillable fillable, long chatId, bool isRegistration = false)
    {
        var progress = new FillingProgress(fillable, chatId, isRegistration);

        await SharedHandlers.SendMessage(progress.GetNextQuestion(), chatId);
    }
}