using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleForum.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//register sqlite
var connectionString = builder.Configuration.GetConnectionString("Sqlite");
builder.Services.AddDbContext<SimpleForumContext>(x => x.UseSqlite(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<SimpleForumContext>();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
