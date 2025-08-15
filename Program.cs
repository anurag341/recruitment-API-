using Microsoft.EntityFrameworkCore;
using RecruiterApi.Data;
using RecruiterApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core InMemory DB for easy local start
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("RecruitersDb"));

// CORS to allow Vite dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("ViteDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Seed sample data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Recruiters.Any())
    {
        db.Recruiters.AddRange(
            new Recruiter { Name = "Priya Sharma", CompanyName = "TechHire", ContactNumber = "9876543210", NoticePeriodDays = 30, ExpectedCtcLpa = 18.5m },
            new Recruiter { Name = "Rahul Mehta", CompanyName = "TalentBridge", ContactNumber = "9123456780", NoticePeriodDays = 60, ExpectedCtcLpa = 22.0m }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ViteDev");

app.MapControllers();

app.Run();