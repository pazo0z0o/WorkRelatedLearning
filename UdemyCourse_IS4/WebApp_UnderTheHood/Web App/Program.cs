using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_App.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();

//injected the in-memory representation
builder.Services.AddDbContext<ApplicationDBContext>(options => 
{
    //add the connection string through the configuration manager object, config from appsettings   
    options.UseSqlServer(config.GetConnectionString("Default")); //Work DB connstring
});

builder.Services.AddDbContext<HomeDBContext>(options =>
{
    //add the connection string through the configuration manager object, config from appsettings
    options.UseSqlServer(config.GetConnectionString("Home")); //Home DB connstring
});
//behaviour of Identity system -- Password, lockout, user email rules etc
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => 
{  
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15);

    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
