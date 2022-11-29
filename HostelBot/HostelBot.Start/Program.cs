using HostelBot.App;
using HostelBot.Ui;
using HostelBot.Ui.TelegramBot;
using Ninject;


namespace HostelBot.Start;

internal class Program
{
    public static void Main(string[] args)
    {
        var container = ConfigureContainer();
        foreach (var ui in  container.GetAll<IUi>())
        {
            ui.Run();
        }
    }

    public static StandardKernel ConfigureContainer()
    {
        var container = new StandardKernel();
        container.Bind<IUi>().To<TelegramUi>().InSingletonScope();
        container.Bind<IApplication>().To<Application>().InSingletonScope();
        /*bind ICommand to AddResidentCommand
        bind ICommand to AddServiceCommand
        bind ICommand to MakeAnnouncmentCommand*/
        return container;
    }
}