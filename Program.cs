using DoAnChuyenNganh.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ĐĂNG KÝ KẾT NỐI CƠ SỞ DỮ LIỆU
builder.Services.AddDbContext<tiem_netContext>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=tongquan}/{action=Index}/{id?}");

app.Run();
