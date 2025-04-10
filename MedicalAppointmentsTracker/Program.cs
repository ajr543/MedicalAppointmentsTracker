using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MedicalAppointmentsTracker.Data;
using MedicalAppointmentsTracker.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MedicalAppointmentsTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointmentsTrackerContext") ?? throw new InvalidOperationException("Connection string 'MedicalAppointmentsTrackerContext' not found.")));
builder.Services.AddDistributedMemoryCache(); // Required for session storage
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
