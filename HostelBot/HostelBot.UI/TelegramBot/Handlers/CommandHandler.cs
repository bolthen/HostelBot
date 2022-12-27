using HostelBot.App;
using HostelBot.Ui.TelegramBot.Commands;
using HostelBot.Ui.TelegramBot.Handlers.Filling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot.Handlers;

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