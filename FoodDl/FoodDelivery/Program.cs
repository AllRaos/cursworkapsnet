using FoodDelivery.Data;
using FoodDelivery.Models;
using FoodDelivery.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Customer","Courier" };
    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    string Email = "admin2@gmail.com";
    string password = "123123_Admin";
    string EmailCourier = "courier2@gmail.com";
    string passwordCourier = "123123_Courier";
    if (await userManager.FindByEmailAsync(Email) == null)
    {
        var Admin = new ApplicationUser();
        Admin.Email = Email;
        Admin.UserName = Email;
        await userManager.CreateAsync(Admin, password);
        userManager.AddToRoleAsync(Admin, "Admin").GetAwaiter().GetResult();
        if (dbContext.Customers.FirstOrDefault(c => c.ApplicationUserId == Admin.Id) == null)
        {
            var customerEntity = new Customer();
            customerEntity.ApplicationUserId = Admin.Id;
            customerEntity.Email = Admin.Email;
            dbContext.Customers.Add(customerEntity);
            dbContext.SaveChanges();
        }
    }
    if (await userManager.FindByEmailAsync(EmailCourier) == null)
    {
        var Courier = new ApplicationUser();
        Courier.Email = EmailCourier;
        Courier.UserName = EmailCourier;
        await userManager.CreateAsync(Courier, passwordCourier);
        userManager.AddToRoleAsync(Courier, "Courier").GetAwaiter().GetResult();
    }
    string[] name = { "Картопля по-домашньому", "Чізбургер", "Овочі на грилі", "Бургер з гострим перцем", "Класичний бургер", "Шашлик курячий на грилі", "Курячі крильця на грилі", "Картопля фрі", "Свинячі ребра на грилі", "Вегетаріанський бургер" };
    float[] price = { 100, 110, 105, 80, 350, 85, 90, 80, 125, 130 };
    string[] category = { };
    int[] netto = { 75, 105, 100, 150, 250, 100, 120, 160, 140, 120 };
    string[] status = { };
    if (!dbContext.Products.Any())
    {
        // Створення та додавання перших 10 продуктів
        for (int i = 0; i <= 9; i++)
        {
            var newProduct = new Product
            {
                Name = name[i],
                Price = price[i],
                Category = "Фастфуд",
                Netto = netto[i],
                Status = "Є в наявності",
                Image = $"{i}.jpg",
            };

            // Збереження нового продукту у базі даних
            dbContext.Products.Add(newProduct);
        }

        dbContext.SaveChanges();
    }
}


app.Run();
