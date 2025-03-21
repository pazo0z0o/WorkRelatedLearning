using Microsoft.AspNetCore.Authorization;
using WebApp_UnderTheHood.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//adding web api httpclient factory
builder.Services.AddHttpClient("OurWebAPI",client =>
{
    client.BaseAddress = new Uri("https://localhost:7292/");

});

//adds default scheme and the one we will be using second
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
    {// it is  good to put it in a constant or appsettings 
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "MyCookieAuth";
        options.LoginPath = "/Account/Login"; // we can specify the loginPath if we want to
        //options.AccessDeniedPath = "/Account/AccessDenied";  the access denied path can be specified
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // For HTTP use CookieSecurePolicy.None
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Path = "/";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5); // set time that would take for a cookie to expire

    }
);
//const for policy as well | Register the authorization service
builder.Services.AddAuthorization(options => {
    options.AddPolicy("MustBelongToHRDepartment", policy =>
                    policy.RequireClaim("Department", "HR"));
    options.AddPolicy("AdminOnly", policy =>
                    policy.RequireClaim("Admin"));
    //Multiple claims for multiple condition satisfaction
    options.AddPolicy("HRManagerOnly", policy => policy
                    .RequireClaim("Department", "HR")
                    .RequireClaim("Manager")
                    .Requirements.Add(new HRManagerProbationRequirement(3)));

});

builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();

//configure session parameters 
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromMinutes(35);
    options.Cookie.IsEssential = true;
});
//configure cors policy if required
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else { app.UseDeveloperExceptionPage(); }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("OpenCorsPolicy");
//middleware that populates the User.Security context 
app.UseAuthentication(); //before the Authorization middleware
app.UseAuthorization();
//add session middleware
app.UseSession();

app.MapRazorPages();

app.Run();
