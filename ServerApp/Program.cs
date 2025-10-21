using ServerApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IBikeRepository, BikeRepositorySQLite>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("policy",
        policy =>
        {
            policy.AllowAnyOrigin();
        });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("policy");

app.UseAuthorization();

app.MapControllers();

app.Run();