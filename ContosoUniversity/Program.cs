using ContosoUniversity.Data;
using ContosoUniversity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configure services
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

//Note these
//Database Option 1: SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Database Option 2: SQLite 
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//Identity Database Option 1: SQL Server
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

//Identity Database Option 2: SQLite 
//builder.Services.AddDbContext<AppIdentityDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection")));

//Important
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => {
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
}).AddEntityFrameworkStores<AppIdentityDbContext>();

//Important
builder.Services.AddScoped<IPasswordValidator<IdentityUser>, CustomPasswordValidator>();
builder.Services.AddScoped<IUserValidator<IdentityUser>, CustomUserValidator>();

var app = builder.Build();

//Configure middleware
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "sortingpage",
    pattern: "Students/OrderBy{sortBy}/Page{page}",
    defaults: new { Controller = "Students", action = "Index", page = 1 });

app.MapControllerRoute(
    name: "sorting",
    pattern: "Students/OrderBy{sortBy}",
    defaults: new { Controller = "Students", action = "Index" });

// e.g. /Students/Page2)
app.MapControllerRoute(
    name: "pagination",
    pattern: "Students/Page{page}",
    defaults: new { Controller = "Students", action = "Index", page = 1 });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Note these
SeedData.EnsurePopulated(app);
SeedIdentityData.EnsurePopulated(app);

app.Run();
