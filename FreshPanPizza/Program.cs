using FreshPanPizza.Entities;
using FreshPanPizza.Helpers;
using FreshPanPizza.Interfaces;
using FreshPanPizza.Repositories;
using FreshPanPizza.Repositories.Implementations;
using FreshPanPizza.Repositories.Interfaces;
using FreshPanPizza.Services.Implementations;
using FreshPanPizza.Services.Interfaces;
using FreshPanPizza.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IUserAccessor, UserAccessor>();
builder.Services.AddTransient<ICatalogService, CatalogService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IFileHelper, FileHelper>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Connecting Database
var connectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<AppDBContext>((options) =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<DbContext, AppDBContext>();

builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IRepository<Item>, Repository<Item>>();
builder.Services.AddTransient<IRepository<Category>, Repository<Category>>();
builder.Services.AddTransient<IRepository<ItemType>, Repository<ItemType>>();
builder.Services.AddTransient<IRepository<CartItem>, Repository<CartItem>>();
builder.Services.AddTransient<IRepository<OrderItem>, Repository<OrderItem>>();
builder.Services.AddTransient<IRepository<PaymentDetails>, Repository<PaymentDetails>>();

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;

var configuration = builder.Configuration;
//var value = configuration.GetValue<string>("value");
builder.Services.Configure<RazorPayConfig>(configuration.GetSection("RazorPayConfig"));

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication(); //Able to access the User information

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=SingleDashboard}/{id?}");

app.Run();
