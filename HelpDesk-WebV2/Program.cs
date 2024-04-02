using HelpDesk_WebV2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Configuration.AddJsonFile("appsettings.json");
var secretkey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretkey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://helpdesk--api.azurewebsites.net/api/Auth/login/");
});

builder.Services.AddHttpClient<EmployeeService>(client =>
{
    client.BaseAddress = new Uri("https://helpdesk--api.azurewebsites.net/api/Empleados");
});

builder.Services.AddHttpClient<OpcionesService>(client =>
{
    client.BaseAddress = new Uri("https://helpdesk--api.azurewebsites.net/api/Opciones");
});

builder.Services.AddHttpClient<RolesService>(client =>
{
    client.BaseAddress = new Uri("https://helpdesk--api.azurewebsites.net/api/Roles");
});

builder.Services.AddHttpClient<TicketsService>(client =>
{
    client.BaseAddress = new Uri("https://helpdesk--api.azurewebsites.net/api/Tickets");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//app.Use(async (context, next) =>
//{
//    if (!context.User.Identity.IsAuthenticated && context.Request.Path.Value != "/Account/Login")
//    {
//        context.Response.Redirect("/Account/Login");
//    }
//    else
//    {
//        await next();
//    }
//});

app.Run();
