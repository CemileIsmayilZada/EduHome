using EduHome.Business.DTOs.CourseDTOs;
using EduHome.Business.Mappers;
using EduHome.Business.Services.Implementations;
using EduHome.Business.Services.Interfaces;
using EduHome.Business.Validations.Courses;
using EduHome.DataAccess.Contexts;
using EduHome.DataAccess.Repositories.Implementations;
using EduHome.DataAccess.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(CoursePostDTOValidator).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conString = builder.Configuration["ConnectionStrings:default"];
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(conString);
});

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddAutoMapper(typeof(CourseMapper).Assembly);
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
