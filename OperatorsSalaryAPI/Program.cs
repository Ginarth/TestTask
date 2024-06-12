using Microsoft.EntityFrameworkCore;
using SupportOperatorsSalaryAPI.Data.Database;
using SupportOperatorsSalaryAPI.Data.Repositories;
using SupportOperatorsSalaryAPI.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ISupportOperatorRepository, SupportOperatorRepository>();
builder.Services.AddTransient<IParameterRepository, ParameterRepository>();
builder.Services.AddTransient<IBaseRateRepository, BaseRateRepository>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();