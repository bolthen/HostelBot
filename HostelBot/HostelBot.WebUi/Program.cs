using HostelBot.App;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Managers;
using HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Ui;
using HostelBot.Ui.TelegramBot;
using Ninject;

AppDomain.CurrentDomain.UnhandledException += ProcessException;

var builder = WebApplication.CreateBuilder(args);
var container = ConfigureContainer();
foreach (var ui in container.GetAll<IResidentUi>())
    Task.Run(() => ui.Run());

builder.Services.AddRazorPages();
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "CookieAuth";
    options.LoginPath = "/Account/Login";
});

builder.Services.AddSingleton<AdministratorRepository>(_ => container.Get<AdministratorRepository>());
builder.Services.AddSingleton(_ => container.Get<MainDbContext>());
builder.Services.AddSingleton(_ => container.Get<ResidentRepository>());
builder.Services.AddSingleton(_ => container.Get<HostelRepository>());
builder.Services.AddSingleton(_ => container.Get<UtilityNameRepository>());
builder.Services.AddSingleton(_ => container.Get<AppealRepository>());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

static StandardKernel ConfigureContainer()
{
    var container = new StandardKernel();
    container.Bind<IResidentUi>().To<TelegramResidentUi>().InSingletonScope();
    container.Bind<IApplication>().To<Application>().InSingletonScope();
     
    container.Bind<Command>().To<ChooseUtilityCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<CheckRegistrationCommand>().ToSelf().WhenInjectedInto<Application>().InSingletonScope();
    
    container.Bind<Manager<UtilityFillable>>().To<UtilityFillableManager>().WhenInjectedInto<ChooseUtilityCommand>().InSingletonScope();
    container.Bind<Manager<AppealFillable>>().To<AppealFillableManager>().WhenInjectedInto<AppealCommand>().InSingletonScope();
    container.Bind<Manager<ResidentFillable>>().To<ResidentFillableManager>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();

    container.Bind<EntityChangesHandler<Appeal>>().ToSelf().InSingletonScope();
    container.Bind<EntityChangesHandler<Resident>>().ToSelf().InSingletonScope();
    container.Bind<RepositoryChangesParser>().ToSelf().InSingletonScope();

    container.Bind<IMainDbContext>().To<MainDbContext>().InSingletonScope();
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