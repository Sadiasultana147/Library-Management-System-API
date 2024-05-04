using LMS_BAL.AuthorsRepo;
using LMS_BAL.BooksRepo;
using LMS_BAL.BorrowdBooksRepo;
using LMS_BAL.MembersRepo;
using LMS_BAL.UserRepo;
using LMS_DAL.AuthorsRepo;
using LMS_DAL.Bookrepo;
using LMS_DAL.BorrowedBooksRepo;
using LMS_DAL.MemberRepo;
using LMS_DAL.UserRepo;
using LMS_DATA;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject IConfiguration into the Startup class
var configuration = builder.Configuration;
// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowdBooksRepository, BorrowdBooksRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
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
