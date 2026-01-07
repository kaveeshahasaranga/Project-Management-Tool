using KanbanApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Services Register කරන තැන ---

// Database Setup
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("KanbanDb"));

// CORS: React (Port 5173) ට එන්න අවසර දෙනවා
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // ඔයාගේ React Port එක මෙතන තියෙන්න ඕන
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 2. App එක Run වෙන විදිය ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// මේක අනිවාර්යයි! CORS ප්‍රතිපත්තිය ක්‍රියාත්මක කරන්න
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.MapControllers();

// --- Database එක හිස් නම්, මුලින්ම Columns ටිකක් හදන්න (Data Seeding) ---
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KanbanApi.Data.AppDbContext>();
    
    // දැනට Columns මුකුත් නැත්නම් විතරක් මේවා දාන්න
    if (!context.Columns.Any())
    {
        context.Columns.Add(new KanbanApi.Models.KanbanColumn { Title = "To Do" });
        context.Columns.Add(new KanbanApi.Models.KanbanColumn { Title = "In Progress" });
        context.Columns.Add(new KanbanApi.Models.KanbanColumn { Title = "Done" });
        context.SaveChanges();
    }
}
// ------------------------------------------------------------------

app.Run(); // මේක තමයි අන්තිම පේළිය