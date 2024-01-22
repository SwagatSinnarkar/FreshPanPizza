using FreshPanPizza.Entities;
using FreshPanPizza.Helpers;
using FreshPanPizza.Interfaces;
using FreshPanPizza.Repositories;
using FreshPanPizza.Services.Implementations;
using FreshPanPizza.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IUserAccessor, UserAccessor>();

//Connecting Database
var connectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<AppDBContext>((options) =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<DbContext, AppDBContext>();


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;

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
app.UseAuthentication(); //Able to access the User information

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
