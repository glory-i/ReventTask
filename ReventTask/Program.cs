using Microsoft.EntityFrameworkCore;
using ReventTask.Data;
using ReventTask.Repositories.Implementation;
using ReventTask.Repositories.Interfaces;
using ReventTask.Services.Implementation;
using ReventTask.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();



builder.Services.AddScoped<ITeacherServices, TeacherServices>();
builder.Services.AddScoped<IClassromServices, ClassroomServices>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(Program).Assembly); //for automapper

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

app.Run();
