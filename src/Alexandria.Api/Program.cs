using Alexandria.Api.Extensions;
using Alexandria.Bussiness.Intefaces.Repositories;
using Alexandria.Bussiness.Interfaces.Notifications;
using Alexandria.Bussiness.Interfaces.Services;
using Alexandria.Bussiness.Notifications;
using Alexandria.Bussiness.Services;
using Alexandria.Identity.Data;
using Alexandria.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Identity
builder.Services.AddDbContext<IdentityDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// NHibernate
builder.Services.AddNHibernate(builder.Configuration);

// DI
builder.Services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddTransient<IBookRepository, BookRepository>();

builder.Services.AddScoped<INotifier, Notifier>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IBookInstanceService, BookInstanceService>();


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
