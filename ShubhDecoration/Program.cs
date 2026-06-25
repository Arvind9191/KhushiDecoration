using Microsoft.EntityFrameworkCore;
using Shubhdecoration.DataAccess.Dapper;
using ShubhDecoration.Helper;
using ShubhDecoration.Programs;
var builder = WebApplication.CreateBuilder(args);

DataHelper.Initialize(builder.Configuration);
EmailSettings.SenderEmail = builder.Configuration["EmailSetting:SenderEmail"];
EmailSettings.Password = builder.Configuration["EmailSetting:Password"];
EmailSettings.Port = builder.Configuration["EmailSetting:Port"];
EmailSettings.Host = builder.Configuration["EmailSetting:Host"];


// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ShubhDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<ConectionModel>(builder.Configuration.GetSection("ConnectionStrings"));
Services.ConfigureServices(builder.Services); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseSession();
app.UseRouting(); 
app.UseAuthorization(); 
app.MapStaticAssets();  
app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Decorator}/{id?}")
    pattern: "{controller=Decoration}/{action=AllDecoration}/{id?}")
    .WithStaticAssets(); 
app.Run();
