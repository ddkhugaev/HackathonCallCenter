using Hackathon.Db;
using HackathonCallCenter.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connection));


// Add services to the container.
builder.Services.AddControllersWithViews();

// добавление репозитория звонков
builder.Services.AddTransient<ICallsRepository, CallsDbRepository>();

// добавление репозитория операторов
builder.Services.AddTransient<IAgentsRepository, AgentsDbRepository>();

// добавления сервиса анализатора
builder.Services.AddSingleton<Analyzer>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
