using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI_Assignment.Contexts;
using WebAPI_Assignment.Middlewares;
using WebAPI_Assignment.Models;
using WebAPI_Assignment.Repositories;
using WebAPI_Assignment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ---> Addera olika tjänster <---
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("MyDb"));
builder.Services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("MyDb"));

builder.Services.AddIdentityApiEndpoints<User>(options =>
{
  options.Password.RequiredLength = 6;
  options.Password.RequireDigit = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireNonAlphanumeric = true;
  options.User.RequireUniqueEmail = true;
}).AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<IdentityContext>();

builder.Services.Configure<BearerTokenOptions>(IdentityConstants.BearerScheme, options => options.BearerTokenExpiration = TimeSpan.FromMinutes(5));

builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAutoMapper(cfg =>
{
  cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODExMzc2MDAwIiwiaWF0IjoiMTc3OTg4Nzk3MiIsImFjY291bnRfaWQiOiIwMTllNjk5MjZlZTQ3MmQ5OGMzNTQwZjVlY2VhNDI3NiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa3Ntc2U3bmd2NmN5Zzcwa2YwYjNoOTIzIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.I4N0gGoLNAbfXV2yokxWkOmlrPDYWQpL0HIVlSg-j-OkKrLbjboNzTyXVNNM5gbYY0GlqxczV2q62ictXrnUNXvvtAwckWY21RHmfd1Z12tJYY0fXameTej6xV-FA5cwZNcVj3u5ZewyXKhfA3pIQIj_Q_ek5nylQdGu0gs4uSVfiSNjrkgotiIsVaaLGI9W6r23gLtKozlVf8vEb37yx6WCwvVoDU3ujYB5-t7RT1JAUrQqN7zAvELe7w-n6GFMDOqkzrwf_cYd5RVej0USJaWp3Fblo8k_ZkMG2XrGZ8YdOVTfh-rtWbka4lal8CTlED6A8LVWWdY-_PswjG1c9w";
  cfg.AddMaps(typeof(Program));
});

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  await dbcontext.Database.EnsureCreatedAsync();

  var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
  await identityContext.Database.EnsureCreatedAsync();

  var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

  string[] roles = ["Admin", "User"];
  foreach (var role in roles)
  {
    if (!await roleManager.RoleExistsAsync(role))
    {
      await roleManager.CreateAsync(new IdentityRole(role));
    }
  }

  var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
  var userId = await userManager.FindByEmailAsync("admin@test.com");
  if (userId is null)
  {
    userId = new User { UserName = "admin@test.com", Email = "admin@test.com" };
    await userManager.CreateAsync(userId, "Admin123!");
  }
  await userManager.AddToRoleAsync(userId, "Admin");

  userId = await userManager.FindByEmailAsync("hans@test.com");
  if (userId is null)
  {
    userId = new User { UserName = "hans@test.com", Email = "hans@test.com" };
    await userManager.CreateAsync(userId, "Test123!");
  }
  await userManager.AddToRoleAsync(userId, "User");

  var cate1 = new Category("Snabb")
  {
    UserId = userId.Id
  };
  var cate2 = new Category("Hemma")
  {
    UserId = userId.Id
  };
  var cate3 = new Category("Borta")
  {
    UserId = userId.Id
  };

  var not1 = new Note("Mat", "Dags att köpa mat", cate1.Id)
  {
    UserId = userId.Id
  };
  var not2 = new Note("Saker", "Att köpa", cate2.Id)
  {
    UserId = userId.Id
  };
  var not3 = new Note("Kläder", "Blå byxor", cate3.Id)
  {
    UserId = userId.Id
  };

  cate1.Notes.Add(not1);
  cate2.Notes.Add(not2);
  cate3.Notes.Add(not3);

  if (!await dbcontext.Categories.AnyAsync())
  {
    await dbcontext.Categories.AddRangeAsync(cate1, cate2, cate3);
    await dbcontext.SaveChangesAsync();
  }
  if (!await dbcontext.Notes.AnyAsync())
  {
    await dbcontext.Notes.AddRangeAsync(not1, not2, not3);
  }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapIdentityApi<User>();
app.UseMiddleware<CheckMiddleware>();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
