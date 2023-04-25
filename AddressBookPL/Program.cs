using AddressBookDL;
using AddressBookDL.InterfacesOfRepo;
using AddressBookEL.IdentityModels;
using AddressBookEL.Mapping;
using AddressBookEL.Models;
using AddressBookEL.ViewModels;
using AddressBookPL.DefaultData;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//sonrdan eklenen
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});

//identity ayarı

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    //ayarlar eklenecek
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "asdfghjklşiqwertyuopğüzxcvbnmöç-_123456789QWERTYUIOPĞÜASDFGHJKLŞİZXCVBNMÖÇ";

}).AddDefaultTokenProviders().AddEntityFrameworkStores<MyContext>();



//auto mapper ayarları

builder.Services.AddAutoMapper(x =>
{
    x.AddExpressionMapping();
    x.AddProfile(typeof(Maps));
});
// Add services to the container.
builder.Services.AddControllersWithViews();

//interfaceların DI için yaşam döngüleri (AddScoped)
//buraya geri döneceğiz



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();//wwwroot

app.UseRouting();

app.UseAuthentication();//login logut
app.UseAuthorization();//yetki

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Proje ilk �al��aca�� zaman default olarak eklenmesini istedi�iniz verileri yada ba�ka i�lemleri yazd���n�z class� burada �a��rmal�s�n�z


//buraya geri d�nece�iz

//app.Data(); // extension metot olarka �a��rmak
//DataDefault.Data(app);  // harici �a��rmak

//Xihan Shen ablan�n y�nteminden yapal�m b�ylece Erdener'in static olmas�n istei�ini yerine getirelim.

using (var scope = app.Services.CreateScope())
{
    //Resolve ASP .NET Core Identity with DI help
    var userManager = (UserManager<AppUser>?)scope.ServiceProvider.GetService(typeof(UserManager<AppUser>));
    var roleManager = (RoleManager<AppRole>?)scope.ServiceProvider.GetService(typeof(RoleManager<AppRole>));
    // do you things here

    DataDefaultXihan d = new DataDefaultXihan();

    d.CheckAndCreateRoles(roleManager);

}


app.Run();//uygulamayı çalıştırır.
