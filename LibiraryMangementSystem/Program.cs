using LibraryManagementSystem.Data;
using LibraryManagementSystem.Profiles;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Repositories.Implementation;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Repo
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBookRepository, BookRepository>();
//unit of work

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Add services to the container
builder.Services.AddControllers();
//add mappers
builder.Services.AddAutoMapper(typeof(Program));
// services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddDbContext<LibraryContext>(options =>
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
