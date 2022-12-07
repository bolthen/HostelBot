using HostelBot.App;
using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Ui;
using HostelBot.Ui.TelegramBot;
using Ninject;

namespace HostelBot.Start;

internal class Program
{
    public static void Main(string[] args)
    {
        var container = ConfigureContainer();
        foreach (var ui in container.GetAll<IUi>())
            ui.Run();
    }

    private static StandardKernel ConfigureContainer()
    {
        var container = new StandardKernel();
        container.Bind<IUi>().To<TelegramUi>().InSingletonScope();
        container.Bind<IApplication>().To<Application>().InSingletonScope();
        
        container.Bind<ServiceManager>().ToSelf().WhenInjectedInto<ChooseServiceCommand>().InSingletonScope();
        
        container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<Command>().To<StatusCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<Command>().To<ChooseServiceCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<Manager<Service>>().To<ServiceManager>().WhenInjectedInto<AppealCommand>().InSingletonScope();
        container.Bind<Manager<Appeal>>().To<AppealManager>().WhenInjectedInto<AppealCommand>().InSingletonScope();
        
        return container;
    }
}