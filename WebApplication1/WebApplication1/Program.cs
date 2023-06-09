using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Shared;
using WebApplication1.AuthorizationRequirements;
using static WebApplication1.AuthorizationRequirements.CustomRequireClaim;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>();



#region Authentication
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
{
    config.Cookie.Name = "Grandmas.Cookie";
    config.LoginPath = "/Home/Authenticate";
});
#endregion

#region AddIdentity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
{   //some configurations on the password == I removed restrictions here 
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    //Email verification now required
    config.SignIn.RequireConfirmedEmail = true;

}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders(); //Registers the services
#endregion



#region Add Authorization
builder.Services.AddAuthorization(config =>
{
    //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
    //var defaultAuthPolicy = defaultAuthBuilder
    //    .RequireAuthenticatedUser()
    //    .RequireClaim(ClaimTypes.DateOfBirth)
    //    .Build();

    //config.DefaultPolicy = defaultAuthPolicy;


    config.AddPolicy("Claim.DoB", policyBuilder =>
    {
        policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.DateOfBirth));
    });

    //config.AddPolicy("Claim.DoB", policyBuilder =>
    //{
    //    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
    //});


});

builder.Services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

builder.Services.AddControllersWithViews();
#endregion



// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting(); // routing middleware
app.UseAuthentication(); // are you allowed in that part of the page?
app.UseAuthorization(); //yes? Ok who are you then?

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
