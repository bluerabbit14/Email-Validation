
using EmailValidationAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Register service
builder.Services.AddSingleton<EmailValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();