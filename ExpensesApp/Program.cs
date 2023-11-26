using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExpensesApp.Data;
using ExpensesApp.Areas.Identity.Data;
using ExpensesApp.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ExpensesAppContextConnection") ?? throw new InvalidOperationException("Connection string 'ExpensesAppContextConnection' not found.");


builder.Services.AddDefaultIdentity<ExpensesAppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ExpensesAppDBContext>();



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(config =>
{

    config.LoginPath = "/Identity/Account/Login";

});
   


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength =8 ;
}
    );



builder.Services.AddDbContext<ExpensesAppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ExpensesAppContextConnection")));
var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VlhiQlVPd11dX2FWfFN0RnNYfVRwc19CZ0wgOX1dQl9gSH9RdEViW3pbcHVXRWQ=");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
