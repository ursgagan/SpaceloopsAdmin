using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpaceLoops.BAL.Services;
using SpaceLoops.DAL.Data;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using SpaceLoops.DAL.Interfaces;
using SpaceLoops.DAL.Repositories;
using System;
using System.Configuration;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ContactServices>();
builder.Services.AddScoped<IRepository<Contact>, ContactRepository>();
builder.Services.AddScoped<AddressServices>();
builder.Services.AddScoped<IAddressRepository<Country>, AddressRepository>();
builder.Services.AddScoped<StateServices>();
builder.Services.AddScoped<IRepository<State>, StateRepository>();
builder.Services.AddScoped<CityServices>();
builder.Services.AddScoped<IRepository<City>, CityRepository>();
builder.Services.AddScoped<IStatesRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();


builder.Services.AddScoped<UserRegistrationService>();
builder.Services.AddScoped<IRepository<UserRegistration>, UserRegistrationRepository>();
builder.Services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();

builder.Services.AddScoped<ImageServices>();
builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Address/UserLogin";
});

var connectionString = builder.Configuration.GetConnectionString("ConnStr");
builder.Services.AddDbContext<SpaceLoopsDbContext>(x =>
{
    x.UseSqlServer(connectionString);
    x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

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

app.Run();
