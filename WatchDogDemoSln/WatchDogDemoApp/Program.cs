using WatchDog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddWatchDogServices(option =>
{
    option.IsAutoClear = true;
    option.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.MSSQL;
    option.SetExternalDbConnString = config.GetConnectionString("WatchDogDb");
    option.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Daily;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWatchDogExceptionLogger();
app.UseWatchDog(option =>
{
    option.WatchPagePassword = "SOME_PASSWORD";
    option.WatchPageUsername = "SOME_USER";
});

app.Run();
