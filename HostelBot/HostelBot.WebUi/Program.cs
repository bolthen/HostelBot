using HostelBot.App;
using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Managers;
using HostelBot.Domain.Infrastructure.Repository;
using HostelBot.Domain.Infrastructure.Services;
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
//foreach (var ui in container.GetAll<IUi>())
//    Task.Run(() => ui.Run());

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
    container.Bind<IUi>().To<TelegramUi>().InSingletonScope();
    container.Bind<IApplication>().To<Application>().InSingletonScope();
        
    container.Bind<UtilityManager>().ToSelf().WhenInjectedInto<ChooseServiceCommand>().InSingletonScope();
        
    container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<StatusCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<ChooseServiceCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<IApplication>().InSingletonScope();
    container.Bind<Command>().To<InformationCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<StatusCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<ChooseServiceCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<AppealCommand>().WhenInjectedInto<CheckRegistrationCommand>().InSingletonScope();
    container.Bind<Command>().To<CheckRegistrationCommand>().WhenInjectedInto<ResidentRegistrationCommand>().InSingletonScope();
    container.Bind<Manager<UtilityFiller>>().To<UtilityManager>().WhenInjectedInto<ChooseServiceCommand>().InSingletonScope();
    container.Bind<Manager<AppealFiller>>().To<AppealManager>().WhenInjectedInto<AppealCommand>().InSingletonScope();
    container.Bind<Manager<ResidentFiller>>().To<ResidentManager>().WhenInjectedInto<ResidentRegistrationCommand>().InSingletonScope();

    container.Bind<MainDbContext>().ToSelf().InSingletonScope();
    container.Bind<IEntityRepository<Resident>>().To<EntityRepository<Resident>>().InSingletonScope();
    container.Bind<IEntityRepository<Hostel>>().To<EntityRepository<Hostel>>().InSingletonScope();
    container.Bind<IEntityRepository<Utility>>().To<EntityRepository<Utility>>().InSingletonScope();
    container.Bind<IEntityRepository<Room>>().To<EntityRepository<Room>>().InSingletonScope();
    container.Bind<IEntityRepository<UtilityName>>().To<EntityRepository<UtilityName>>().InSingletonScope();

    container.Bind<UtilityRepository>().ToSelf().InSingletonScope();
    container.Bind<ResidentRepository>().ToSelf().InSingletonScope();
    container.Bind<HostelRepository>().ToSelf().InSingletonScope();
    container.Bind<UtilityNameRepository>().ToSelf().InSingletonScope();

    return container;
}
