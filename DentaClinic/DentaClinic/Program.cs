using DentaClinic.Controllers;
using DentaClinic.Database;
using DentaClinic.Models;
using DentaClinic.Repositories;
using DentaClinic.Utils;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebDbContext>(options => 
{
    var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connetionString);
});

builder.Services.AddScoped<PatientCardController>();
builder.Services.AddScoped<VisitController>();
builder.Services.AddScoped<FeedbackController>();
builder.Services.AddScoped<IPatientCardRepository, PatientCardRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
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

app.MapControllers();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
