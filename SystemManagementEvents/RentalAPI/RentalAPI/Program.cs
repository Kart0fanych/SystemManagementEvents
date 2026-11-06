using RentalApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Регистрация служб
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Хранилище в памяти (одно на весь сервис)
builder.Services.AddSingleton<IEquipmentRepository, InMemoryEquipmentRepository>();

var app = builder.Build();

// Swagger только в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();