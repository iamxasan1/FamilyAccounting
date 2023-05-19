using FamilyAccounting.Data.Context;
using FamilyAccounting.Data.Entities;
using FamilyAccounting.Data.Helpers;
using FamilyAccouting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=Family1;Username=postgres;Password=Admin123");

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    //options.UseSqlServer("Server=sql.bsite.net\\MSSQL2016; Database=iamxasan1_Family_Accounting; User Id=iamxasan1_Family_Accounting; Password=asd123");
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Users/SignIn";
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserProvider>();
builder.Services.AddScoped<DateDto>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
