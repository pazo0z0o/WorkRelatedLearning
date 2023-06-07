

using IdentityExample.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(config =>
    {
        config.UseInMemoryDatabase("Memory");
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders();

//cookieHandler
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth",config => 
{
    config.Cookie.Name = "Grandmas.Cookie";
    config.LoginPath = "/Home/Authenticate";
});

builder.Services.AddAuthorization(config =>
{//builder pattern -- created a default Authorization policy
    var defaultAuthBuilder = new AuthorizationPolicyBuilder();
    var defaultAuthPolicy = defaultAuthBuilder
    .RequireAuthenticatedUser()
    .RequireClaim(ClaimTypes.DateOfBirth)
    .Build(); //need an authenticated user to continue -- part of the default policy

    config.DefaultPolicy = defaultAuthPolicy;
});

builder.Services.AddControllersWithViews();


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
