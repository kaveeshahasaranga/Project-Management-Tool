using KanbanApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Services Register කරන තැන (කුස්සියට බඩු ගේනවා) ---

// Database එක සම්බන්ධ කරනවා
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("KanbanDb"));

// Controllers වැඩ කරන්න ඕන කියලා කියනවා (මේක නැති නිසා තමයි කලින් අවුල් ගියේ)
builder.Services.AddControllers();

// Swagger (Menu Card) එක හදාගන්නවා
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 2. App එක Run වෙන විදිය (Restaurant එක වැඩ කරන විදිය) ---

// Development එකේදී විතරක් Swagger පෙන්නන්න
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Controllers වලට පාර පෙන්නන්න (Map)
app.MapControllers();

app.Run();