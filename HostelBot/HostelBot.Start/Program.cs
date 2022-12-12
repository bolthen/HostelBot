using HostelBot.App;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Domain.Infrastructure.Services;
using HostelBot.Ui;
using HostelBot.Ui.TelegramBot;
using Microsoft.EntityFrameworkCore;
using Ninject;

namespace HostelBot.Start;

internal class Program
{
    public static void Main(string[] args)
    {
        // var container = ConfigureContainer();
        // foreach (var ui in  container.GetAll<IUi>())
        // {
        //     ui.Run();
        // }
        

        var mainDbContext = new MainDbContext();
        var coreRepository = new CoreRepository(mainDbContext, new EntityRepository<Resident, int>(mainDbContext),
            new EntityRepository<Hostel, string>(mainDbContext), new EntityRepository<Room, int>(mainDbContext),
            new EntityRepository<Service, string>(mainDbContext));
        
        var residentService = new ResidentService(coreRepository);
        var hostel = new Hostel("6 общага");
        var room = new Room(708, hostel);
        var res = new Resident(13, "Egor", "Loparev", room, hostel);
        var k = residentService.CreateAsync(res);
        var g= residentService.GetAsync(13);
        Console.WriteLine(k.Result);
    }

    public static StandardKernel ConfigureContainer()
    {
        var container = new StandardKernel();
        container.Bind<IUi>().To<TelegramUi>().InSingletonScope();
        container.Bind<IApplication>().To<Application>().InSingletonScope();
        
        container.Bind<ICommand>().To<InformationCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<ICommand>().To<StatusCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<ICommand>().To<AppealCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        container.Bind<ICommand>().To<ChooseServiceCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
        
        
        // container.Bind<IEntityRepository<Hostel, string>>().To<EntityRepository<Hostel, string>>().InSingletonScope();
        // container.Bind<IEntityRepository<Resident, int>>().To<EntityRepository<Resident, int>>().InSingletonScope();
        
        /*bind ICommand to AddResidentCommand
        bind ICommand to AddServiceCommand
        bind ICommand to MakeAnnouncmentCommand*/
        return container;
    }
}