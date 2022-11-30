namespace HostelBot.Ui.TelegramBot;

internal static class KeyboardButtons
{
    public static void GetConfigurators()
    {
        /*commands = app.getUserCommands();
          foreach command in commands:
            button = new Button(command.createConfiguration)
            buttons[button.description] = button;
            
          class Button() {
            OnButtonClicked(...) {
                Сериализация нажатых полей
                в конце всех введенных данных configurator.Parse(сериализованная строка)
            }
          }
        */
        
    }

    public static class Services
    {
        public const string Janitor = "Уборщица";
        public const string Electrician = "Электрик";
        public const string Santekhnik = "Сантехник";
    }
}