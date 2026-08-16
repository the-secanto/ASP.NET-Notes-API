using Microsoft.EntityFrameworkCore;
using NotesApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured. Set it via environment variables or appsettings.Development.json.");
}

builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();
var shouldSeedDatabase = args.Contains("--seed", StringComparer.OrdinalIgnoreCase);

if (shouldSeedDatabase)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
    await DatabaseSeeder.SeedAsync(dbContext);
    return;
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/test", () => "This is a test to see the difference between Controller and regular mapping");
app.MapControllers();

app.Run();
