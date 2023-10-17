using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductAPIDemo.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductAPIDemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductAPIDemoContext") ?? throw new InvalidOperationException("Connection string 'ProductAPIDemoContext' not found.")));


///////////////
//builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
//{
//    build.WithOrigins("*");
//    build.AllowAnyMethod();
//    build.AllowAnyHeader();
//}));





//single doiman ("https://localhost:7170/")
//multiple doiman ("https://localhost:7170/ , https://localhost:7170/ , https://localhost:7170/)
//any doiman  ("*")
///////////////////
///
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

///////////

app.UseCors("corspolicy");

/////////////
app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
