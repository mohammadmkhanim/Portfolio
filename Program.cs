using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Context;
using Portfolio.Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<PortfolioDBContext>(option =>
         option.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDBConnectionString")));

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(context =>
    new UnitOfWork(builder.Services.BuildServiceProvider().GetRequiredService<PortfolioDBContext>()));

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
    name: "Management",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
