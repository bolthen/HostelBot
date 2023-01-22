using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Exceptions;
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
            case StepByStepFilling.CurrentProgressStatus.WrittenDown:
                await SharedHandlers.SendMessage(progress.GetNextQuestion(), chatId);
                return;
            case StepByStepFilling.CurrentProgressStatus.RegexFailed:
                await SharedHandlers.SendMessage(result.errorMessage!, chatId);
                return;
            case StepByStepFilling.CurrentProgressStatus.Completed:
                
                var answers = progress.GetAnswers();
                var fillable = progress.fillable;
                FillingProgress.FinishFilling(chatId);

                // await SharedHandlers.SendMessage(answers.ToJsonFormat(), chatId);
                
                try
                {
                    fillable.FillClass(answers);
                }
                catch (InvalidHostelNameException)
                {
                    await SharedHandlers.SendMessage("Общежития с таким номером не существует", chatId);
                    return;
                }
                
                if (progress.IsRegistration)
                    await SharedHandlers.WaitVerification(chatId);
                
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static async Task StartFilling(IFillable fillable, long chatId, bool isRegistration = false)
    {
        var progress = new FillingProgress(fillable, chatId, isRegistration);

        await SharedHandlers.SendMessage(progress.GetNextQuestion(), chatId);
    }
}