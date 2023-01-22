using HostelBot.App;
using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Managers;
using HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Ui;
using HostelBot.Ui.TelegramBot;
using Ninject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "CookieAuth";
    options.LoginPath = "/Account/Login";
});


var container = ConfigureContainer();
foreach (var ui in container.GetAll<IResidentUi>())
    Task.Run(() => ui.Run());

builder.Services.AddSingleton<AdministratorRepository>();
builder.Services.AddSingleton(_ => container.Get<IMainDbContext>());
builder.Services.AddSingleton(_ => container.Get<ResidentRepository>());
builder.Services.AddSingleton(_ => container.Get<HostelRepository>());
builder.Services.AddSingleton(_ => container.Get<UtilityNameRepository>());

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();


static StandardKernel ConfigureContainer()
{
    var container = new StandardKernel();
    container.Bind<IResidentUi>().To<TelegramResidentUi>().InSingletonScope();
    container.Bind<IApplication>().To<Application>().InSingletonScope();
        
    container.Bind<FillableUtilityManager>().ToSelf().WhenInjectedInto<ChooseUtilityCommand>().InSingletonScope();
        
    container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<StatusCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<ChooseUtilityCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<StatusCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<ChooseUtilityCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<CheckRegistrationCommand>().ToSelf().WhenInjectedInto<Application>().InSingletonScope();
    container.Bind<Manager<UtilityFillable>>().To<FillableUtilityManager>().WhenInjectedInto<ChooseUtilityCommand>().InSingletonScope();
    container.Bind<Manager<AppealFillable>>().To<FillableAppealManager>().WhenInjectedInto<AppealCommand>().InSingletonScope();
    container.Bind<Manager<ResidentFillable>>().To<FillableResidentManager>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();

    container.Bind<IMainDbContext>().To<MainDbContext>().InSingletonScope();

    container.Bind<EntityChangesHandler<Appeal>>().ToSelf().InSingletonScope();
    container.Bind<EntityChangesHandler<Resident>>().ToSelf().InSingletonScope();
    container.Bind<RepositoryChangesParser>().ToSelf().InSingletonScope();
    /*container.Bind<AppealChangesHandler>().ToSelf().InSingletonScope();
    container.Bind<ResidentChangesHandler>().ToSelf().InSingletonScope();*/

    container.Bind<UtilityRepository>().ToSelf().InSingletonScope();
    container.Bind<ResidentRepository>().ToSelf().InSingletonScope();
    container.Bind<HostelRepository>().ToSelf().InSingletonScope();
    container.Bind<UtilityNameRepository>().ToSelf().InSingletonScope();
    container.Bind<AppealRepository>().ToSelf().InSingletonScope();

    return container;
}
