
using Microsoft.EntityFrameworkCore;
using projectmvcrestruants.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionstring = builder.Configuration.GetConnectionString("Accounts");
builder.Services.AddDbContext<Account>(
options => options.UseSqlServer(connectionstring).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
LogLevel.Information).EnableSensitiveDataLogging());
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // The session will expire after 30 minutes of inactivity
	options.Cookie.HttpOnly = true; // Session cookie is not accessible via client-side scripts
	options.Cookie.IsEssential = true; // Ensures the cookie is stored even if the user hasn't consented to tracking
});



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
