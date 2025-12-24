using Microsoft.EntityFrameworkCore;
using ODEVDAGITIM06.Data;
using ODEVDAGITIM06.Models; // ApplicationUser için
using ODEVDAGITIM06.Repositories;
using ODEVDAGITIM06.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity; // Identity için
using ODEVDAGITIM06.Hubs; // 1. ADIMDA OLUÞTURDUÐUN HUB ÝÇÝN EKLEDÝK

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- SIGNALR SERVÝSÝNÝ EKLEDÝK ---
builder.Services.AddSignalR();

// Veritabaný Baðlantýsý
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- IDENTITY KURULUMU BAÞLANGIÇ ---
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
// --- IDENTITY KURULUMU BÝTÝÞ ---

// Repository'ler
builder.Services.AddScoped<IDersRepository, DersRepository>();
builder.Services.AddScoped<IOdevRepository, OdevRepository>();
builder.Services.AddScoped<ITeslimRepository, TeslimRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// --- SIGNALR HUB ROTASINI TANIMLADIK ---
app.MapHub<BildirimHub>("/bildirimHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();