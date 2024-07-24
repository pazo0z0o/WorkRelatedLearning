var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//adds default scheme and the one we will be using second
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
    {// it is  good to put it in a constant or appsettings 
        options.Cookie.Name = "MyCookieAuth";
        //options.LoginPath = "/Account/Login";

    }
);
//const for policy as well | Register the authorization service
builder.Services.AddAuthorization(options => {
    options.AddPolicy("MustBelongToHRDepartment", policy =>
                    policy.RequireClaim("Department", "HR"));
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

//middleware that populates the User.Security context 
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
