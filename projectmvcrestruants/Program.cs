
using Microsoft.EntityFrameworkCore;
using projectmvcrestruants.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionstring = builder.Configuration.GetConnectionString("registration");
builder.Services.AddDbContext<Registration>(
options => options.UseSqlServer(connectionstring).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
LogLevel.Information).EnableSensitiveDataLogging());




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
