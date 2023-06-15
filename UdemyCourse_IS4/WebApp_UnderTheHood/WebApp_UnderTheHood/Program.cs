using Microsoft.AspNetCore.Authorization;
using WebApp_UnderTheHood.Authorization;
using static WebApp_UnderTheHood.Authorization.HRManagerProbationRequirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Authentication section
builder.Services.AddAuthentication("MyCookieAuth")  //need to add the handler in the services container 
    .AddCookie("MyCookieAuth", options =>  //"MyCookieAuth" = authentication scheme for the middleware
    {
        options.Cookie.Name = "MyCookieAuth";
        options.LoginPath = "/Account/Login"; 
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2); //controls the lifetime of the cookie
    });
#endregion
#region AUTH 
builder.Services.AddAuthorization(options =>
{   //HR department policy
    options.AddPolicy("MustBelongToHRDepartment",
        policy => policy.RequireClaim("Department","HR"));  //Requires the Department value to be "HR "

    //Admin policy
    options.AddPolicy("AdminOnly",
        policy => policy.RequireClaim("Admin"));

    options.AddPolicy("HRManagerOnly",
        policy => policy
        .RequireClaim("Department", "HR") //Requires the Department value to be "HR "
        .RequireClaim("Manager"));
    
    options.AddPolicy("HRManagerOnly",
        policy => policy
        .RequireClaim("Department", "HR") //Requires the Department value to be "HR "
        .RequireClaim("Manager")
        .Requirements.Add(new HRManagerProbationRequirements(3))); //added custom requirement we created
});

//injected our own Handler
builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementsHandler>();
#endregion
builder.Services.AddRazorPages();
//our client points to 
builder.Services.AddHttpClient("OurWebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7224/");
    #region HTTPS/ssl certificates fix
}).ConfigureHttpMessageHandlerBuilder(builder => {
    builder.PrimaryHandler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
    };
});
#endregion

#region app 
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
   
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
#endregion