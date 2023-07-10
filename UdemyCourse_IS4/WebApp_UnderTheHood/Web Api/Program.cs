using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configSecurityKey = builder.Configuration.GetSection("SecretKey").GetValue<string>("SecretKey");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
       // IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("configSecurityKey"))//Configuration.GetValue<string>("SecretKey")))
            IssuerSigningKey = new SymmetricKeyWrapProvider(Encoding.ASCII.GetBytes("configSecurityKey"))//Configuration.GetValue<string>("SecretKey")))

    };
});
builder.Services.AddSwaggerGen( c => 
{
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WenAPI", Version = "v1" });
});

#region app section 
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseAuthentication(); //middleware for authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion