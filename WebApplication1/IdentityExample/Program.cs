using IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(config => {
    config.UseInMemoryDatabase("Memory");
});
//cookieHandler
//builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
//{
//    config.Cookie.Name = "Grandmas.Cookie";
//    config.LoginPath = "/Home/Authenticate";
//});


//Registers the services -- Add Identity for user and his role
builder.Services.AddIdentity<IdentityUser, IdentityRole>(config => 
{   //some configurations on the password == I removed restrictions here 
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;


}) //Registers the services
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config => {

    config.Cookie.Name = "Identity.Cookie";
    config.LoginPath = "/Home/Login";
});


// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
// are you allowed in that part of the page?
app.UseAuthentication();

app.UseAuthorization();
//yes? Ok who are you then?

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
