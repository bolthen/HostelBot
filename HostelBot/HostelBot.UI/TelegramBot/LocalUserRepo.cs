namespace HostelBot.Ui.TelegramBot;

internal static class LocalUserRepo
{
    private static readonly Dictionary<long, bool> ChatId2IsUserRegistered = new();

    public static bool ContainsUser(long chatId)
    {
        return ChatId2IsUserRegistered.ContainsKey(chatId);
    }

    public static void RegisterUser(long chatId)
    {
        ChatId2IsUserRegistered[chatId] = true;
    }
}
