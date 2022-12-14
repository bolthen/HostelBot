using HostelBot.App;
using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Domain.Infrastructure.Services;
using HostelBot.Ui;
using HostelBot.Ui.TelegramBot;
using Ninject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddSingleton<MainDbContext>();
// builder.Services.AddSingleton<EntityRepository<Resident>>();
// builder.Services.AddSingleton<EntityRepository<Hostel>>();
// builder.Services.AddSingleton<EntityRepository<Utility>>();
// builder.Services.AddSingleton<EntityRepository<Room>>();
// builder.Services.AddSingleton<CoreRepository>();
// builder.Services.AddSingleton<ResidentService>();

var container = ConfigureContainer();
foreach (var ui in container.GetAll<IUi>())
    Task.Run(() => ui.Run());

builder.Services.AddSingleton(_ => container.Get<ResidentService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//StartTgUi();

app.Run();

static void StartTgUi()
{
    var container = ConfigureContainer();
    foreach (var ui in container.GetAll<IUi>())
        Task.Run(() => ui.Run());
}

static StandardKernel ConfigureContainer()
{
    var container = new StandardKernel();
    container.Bind<IUi>().To<TelegramUi>().InSingletonScope();
    container.Bind<IApplication>().To<Application>().InSingletonScope();
        
    container.Bind<ServiceManager>().ToSelf().WhenInjectedInto<ChooseServiceCommand>().InSingletonScope();
        
    container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<StatusCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<ChooseServiceCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Manager<Utility>>().To<ServiceManager>().WhenInjectedInto<ChooseServiceCommand>().InSingletonScope();
    container.Bind<Manager<Appeal>>().To<AppealManager>().WhenInjectedInto<AppealCommand>().InSingletonScope();
    
    container.Bind<MainDbContext>().ToSelf().InSingletonScope();
    container.Bind<IEntityRepository<Resident>>().To<EntityRepository<Resident>>().InSingletonScope();
    container.Bind<IEntityRepository<Hostel>>().To<EntityRepository<Hostel>>().InSingletonScope();
    container.Bind<IEntityRepository<Utility>>().To<EntityRepository<Utility>>().InSingletonScope();
    container.Bind<IEntityRepository<Room>>().To<EntityRepository<Room>>().InSingletonScope();
    container.Bind<CoreRepository>().ToSelf().InSingletonScope();
    container.Bind<ResidentService>().ToSelf().InSingletonScope();
    
    return container;
}