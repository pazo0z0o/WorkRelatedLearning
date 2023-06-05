using IdentityExample;
using IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllersWithViews();
#region Establish InMemory database 
builder.Services.AddDbContext<AppDbContext>(config => {
    config.UseInMemoryDatabase("Memory");
});
#endregion
#region WebApplication1 remnant
//cookieHandler
//builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
//{
//    config.Cookie.Name = "Grandmas.Cookie";
//    config.LoginPath = "/Home/Authenticate";
//});
#endregion

//Registers the services -- Add Identity for user and his role
builder.Services.AddIdentity<IdentityUser, IdentityRole>(config => 
{   //some configurations on the password == I removed restrictions here 
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    //Email verification now required
    config.SignIn.RequireConfirmedEmail = true;  

}) //Registers the services
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
#region cookie configuration
builder.Services.ConfigureApplicationCookie(config => {

    config.Cookie.Name = "Identity.Cookie";
    config.LoginPath = "/Home/Login";
});
#endregion

#region MailKit Add with its options 
var MailKitOptions = builder.Configuration.GetSection("Email").Get<MailKitOptions>();  //Gets the email value from appsettings.json and parses it 
//Great case for storage in appsettings.json -- it would have all the params from he MailKitOptions class constructor
builder.Services.AddMailKit(config => { config.UseMailKit(MailKitOptions); });
#endregion   

// Configure the HTTP request pipeline.
#region app commands 
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
#endregion