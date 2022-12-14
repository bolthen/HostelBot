using HostelBot.App;
using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Domain.Infrastructure.Services;
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
        
        container.Bind<UtilityManager>().ToSelf().WhenInjectedInto<ChooseServiceCommand>().InSingletonScope();
        
        container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<Command>().To<StatusCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<Command>().To<ChooseServiceCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<Manager<Utility>>().To<UtilityManager>().WhenInjectedInto<ChooseServiceCommand>().InSingletonScope();
        container.Bind<Manager<Appeal>>().To<AppealManager>().WhenInjectedInto<AppealCommand>().InSingletonScope();

        container.Bind<MainDbContext>().ToSelf().InSingletonScope();
        
        container.Bind<IEntityRepository<Hostel>>().To<EntityRepository<Hostel>>().InSingletonScope();
        container.Bind<IEntityRepository<Resident>>().To<EntityRepository<Resident>>().InSingletonScope();
        container.Bind<IEntityRepository<Room>>().To<EntityRepository<Room>>().InSingletonScope();
        container.Bind<IEntityRepository<Utility>>().To<EntityRepository<Utility>>().InSingletonScope();

        container.Bind<CoreRepository>().To<CoreRepository>().InSingletonScope();

        container.Bind<UtilityService>().ToSelf().InSingletonScope();
        container.Bind<ResidentService>().ToSelf().InSingletonScope();
        container.Bind<HostelService>().ToSelf().InSingletonScope();
        return container;
    }
}