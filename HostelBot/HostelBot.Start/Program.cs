using HostelBot.App;
using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Managers;
using HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Ui;
using HostelBot.Ui.TelegramBot;
using Ninject;

AppDomain.CurrentDomain.UnhandledException += ProcessException;

var container = ConfigureContainer();
var tmp = container.GetAll<HostelRepository>().ToList()[0];
var tasks = container.GetAll<IResidentUi>().Select(ui => Task.Factory.StartNew(ui.Run));

Task.WaitAll(tasks.ToArray());

static StandardKernel ConfigureContainer()
{
    var container = new StandardKernel();
    container.Bind<IResidentUi>().To<TelegramResidentUi>().InSingletonScope();
    container.Bind<IApplication>().To<Application>().InSingletonScope();
        
    container.Bind<UtilityFillableManager>().ToSelf().WhenInjectedInto<ChooseUtilityCommand>().InSingletonScope();
    
    container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    //container.Bind<Command>().To<StatusCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<ChooseUtilityCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<CheckRegistrationCommand>().ToSelf().WhenInjectedInto<Application>().InSingletonScope();
    
    container.Bind<Manager<UtilityFillable>>().To<UtilityFillableManager>().WhenInjectedInto<ChooseUtilityCommand>().InSingletonScope();
    container.Bind<Manager<AppealFillable>>().To<AppealFillableManager>().WhenInjectedInto<AppealCommand>().InSingletonScope();
    container.Bind<Manager<ResidentFillable>>().To<ResidentFillableManager>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();

    //container.Bind<MainDbContext>().ToSelf().InSingletonScope();

    container.Bind<EntityChangesHandler<Appeal>>().ToSelf().InSingletonScope();
    container.Bind<EntityChangesHandler<Resident>>().ToSelf().InSingletonScope();
    container.Bind<RepositoryChangesParser>().ToSelf().InSingletonScope();

    /*container.Bind<IEntityRepository<Resident>>().To<EntityRepository<Resident>>().InSingletonScope();
    container.Bind<IEntityRepository<Hostel>>().To<EntityRepository<Hostel>>().InSingletonScope();
    container.Bind<IEntityRepository<Utility>>().To<EntityRepository<Utility>>().InSingletonScope();
    container.Bind<IEntityRepository<Room>>().To<EntityRepository<Room>>().InSingletonScope();
    container.Bind<IEntityRepository<UtilityName>>().To<EntityRepository<UtilityName>>().InSingletonScope();
    container.Bind<IEntityRepository<Appeal>>().To<EntityRepository<Appeal>>().InSingletonScope();*/

    /*container.Bind<UtilityRepository>().ToSelf().InSingletonScope();
    container.Bind<ResidentRepository>().ToSelf().InSingletonScope();
    container.Bind<HostelRepository>().ToSelf().InSingletonScope();
    container.Bind<UtilityNameRepository>().ToSelf().InSingletonScope();
    container.Bind<AppealRepository>().ToSelf().InSingletonScope();
    container.Bind<AdministratorRepository>().ToSelf().InSingletonScope();*/
    
    
    container.Bind<MainDbContext>().ToSelf().InSingletonScope();
    container.Bind<AdministratorRepository>().ToSelf().InSingletonScope();
    container.Bind<UtilityRepository>().ToSelf().InSingletonScope();
    container.Bind<HostelRepository>().ToSelf().InSingletonScope();
    container.Bind<UtilityNameRepository>().ToSelf().InSingletonScope();
    container.Bind<AppealRepository>().ToSelf().InSingletonScope();
    
    return container;
}

static void ProcessException(object sender, UnhandledExceptionEventArgs args)
{
    Console.WriteLine((args.ExceptionObject as Exception).StackTrace);
    Environment.Exit(0);
}