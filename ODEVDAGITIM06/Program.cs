using Microsoft.EntityFrameworkCore;
using ODEVDAGITIM06.Data;
using ODEVDAGITIM06.Repositories;
using ODEVDAGITIM06.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies; // 1. EKLENDÝ

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Veritabaný baðlantýsýný (DbContext) servislere ekliyoruz.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository'lerimizi servislere ekliyoruz.
builder.Services.AddScoped<IDersRepository, DersRepository>();
builder.Services.AddScoped<IOdevRepository, OdevRepository>();
builder.Services.AddScoped<ITeslimRepository, TeslimRepository>();

// Cookie bazlý kimlik doðrulama servisini ekliyoruz (2. EKLENDÝ)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Giriþ yapýlmamýþsa yönlendirilecek sayfa
        options.LogoutPath = "/Login/Logout"; // Çýkýþ yapma sayfasý
        options.AccessDeniedPath = "/Login/AccessDenied"; // Yetkisi yoksa yönlendirilecek sayfa
        options.Cookie.Name = "OdevPortaliCookie"; // Oluþturulacak çerezin adý
    });


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

app.UseAuthentication(); // 3. EKLENDÝ (UseAuthorization'dan önce olmalý)

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();