global using WebApp1.Models;
using WebApp1.Data;
using WebApp1.Services;
using WebApp1.Services.New;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IProductService,ProductService>();

//builder.Services.AddSingleton<ITestDI, TestDI>();
//builder.Services.AddScoped<ITestDI, TestDI>();
builder.Services.AddTransient<ITestDI, TestDI>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<INewProductService,NewProductService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
