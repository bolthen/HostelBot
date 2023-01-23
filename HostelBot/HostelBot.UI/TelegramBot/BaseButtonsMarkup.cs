using HostelBot.Ui.TelegramBot.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace HostelBot.Ui.TelegramBot;

internal static class BaseButtonsMarkup
{
    public static ReplyKeyboardMarkup Get(long chatId)
    {
        var buttons = BaseCommands.GetBaseCommandsNames(chatId)
            .Select(name => new[] { new KeyboardButton(name) });
        
        return new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false
        };
    }
}