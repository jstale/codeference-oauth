using GoogleOAuth.Services;
using GoogleOIDC.Services.OAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OAuthConfiguration>(
    builder.Configuration.GetSection("OAuth"));
builder.Services.AddScoped<IOidcService, OidcService>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuthScheme")
    .AddCookie("CookieAuthScheme", options =>
    {
        options.LoginPath = "/Account/Login"; // Set the login page URL
        options.AccessDeniedPath = "/Account/AccessDenied"; // Set the access denied page URL
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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//We need this to enable route attributes
app.MapControllers();
app.Run();