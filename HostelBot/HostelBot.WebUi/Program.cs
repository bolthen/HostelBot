using HostelBot.Domain.Infrastructure.Repository;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
{
    options.Cookie.Name = "CookieAuth";
    options.LoginPath = "/Account/Login";
});

builder.Services.AddSingleton<IMainDbContext>(_ => new MainDbContext());
builder.Services.AddSingleton<AdministratorRepository>();
builder.Services.AddSingleton<ResidentRepository>();
builder.Services.AddSingleton<HostelRepository>();
builder.Services.AddSingleton<UtilityNameRepository>();
builder.Services.AddSingleton<AppealRepository>();

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
