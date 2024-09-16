using Microsoft.EntityFrameworkCore;
using Thunders.Test.Task.Manager.Infra.EF;
using Thunders.Test.TaskManager.Infra.IoC.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ThundersDbContext>(opt => opt.UseInMemoryDatabase("thunders"));



DependenceInjection.InjectDependences(builder.Services);

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowAnyOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();