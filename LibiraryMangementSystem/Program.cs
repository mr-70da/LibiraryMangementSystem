
using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Interfaces.Services;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Infrastructure.Data;
using LibraryManagementSystem.Infrastructure.Repositories;
using LibraryManagementSystem.Infrastructure.Repositories.Implementation;
using LibraryManagementSystem.Infrastructure.Services;
using LibraryManagementSystem.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//Repo
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//unit of work

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Add services to the container
builder.Services.AddControllers();
//add mappers
// Replace the incorrect AddAutoMapper line with the correct usage
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")));

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