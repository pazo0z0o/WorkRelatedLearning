using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WevAPP.Data;

var builder = WebApplication.CreateBuilder(args);

// Load appsettings.Development.json or appsettings.json depending on the environment
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddRazorPages();

//inject the db context with the conn string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

});
//inject the IdentityUser and Roles -- can be extendable if I want my own users and roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{//add different options for a ton of things like lockout,password and user 
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    //validation of User Email
    options.User.RequireUniqueEmail = true;
    //sign in options - needed for email registration
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
//configuring the cookie options => many many other options
builder.Services.ConfigureApplicationCookie(options =>
{ 
options.LoginPath = "/Account/Login";
options.AccessDeniedPath ="/Account/AccessDenied";
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
