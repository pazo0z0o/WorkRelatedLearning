

using IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(config =>
    {
        config.UseInMemoryDatabase("Memory");
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders();



builder.Services.AddControllersWithViews();
//cookieHandler
//builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth",config => 
//{
//    config.Cookie.Name = "Grandmas.Cookie";
//    config.LoginPath = "/Home/Authenticate";
//});
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
