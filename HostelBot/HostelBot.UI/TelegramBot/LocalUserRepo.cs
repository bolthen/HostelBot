namespace HostelBot.Ui.TelegramBot;

public static class LocalUserRepo
{
    private static readonly Dictionary<long, LocalUser> ChatId2User = new();

    public static bool ContainsUser(long chatId)
    {
        return ChatId2User.ContainsKey(chatId);
    }

    public static void AddUser(long chatId)
    {
        ChatId2User[chatId] = new LocalUser { Registered = false };
    }

    public static void RegisterUser(long chatId)
    {
        ChatId2User[chatId].Registered = true;
    }

    public static bool IsRegistered(long chatId)
    {
        return ChatId2User[chatId].Registered;
    }
    
    private class LocalUser
    {
        public bool Registered { get; set; }
    }
}
