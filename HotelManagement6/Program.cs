using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Areas.Identity.Data;
using HotelManagement6.Models;
using HotelManagement6.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Components.Forms;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseMySql(connectionString,new MySqlServerVersion(new Version(8,0, 36))));
builder.Services.AddDbContext<HmContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddDefaultIdentity<MySqlIdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

// Add Policies
builder.Services.AddAuthorization(options => {
   //options.AddPolicy("AdminRequired", policy => policy.RequireRole("Admin"));
});
// Add services to the container.
builder.Services.AddRazorPages(options => {
    //options.Conventions.AuthorizeFolder("/Rooms", "AdminRequired");
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
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

using(var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Guest"};
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
        
    }
}
using (var scope = app.Services.CreateScope())
{

    //Why does Admin role only work like this 
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MySqlIdentityUser>>();
    string email = "Admin@aol.com";
    string password = "Hello123,";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new MySqlIdentityUser();
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password);
        // Right now default is everyone who makes an account is Guest change to Admin for special permissions
        await userManager.AddToRoleAsync(user, "Admin");

    }
    // Trying to figure out why Guest Role only works like this
    //    string guestEmail = "guest@example.com";
    //    string guestPassword = "Test1234,";
    //    if (await userManager.FindByEmailAsync(guestEmail) == null)
    //    {
    //        var guestUser = new MySqlIdentityUser();
    //        guestUser.UserName = guestEmail;
    //        guestUser.Email = guestEmail;

    //        await userManager.CreateAsync(guestUser, guestPassword);
    //        await userManager.AddToRoleAsync(guestUser, "Guest");
    //    }
    //}

    app.Run();

}