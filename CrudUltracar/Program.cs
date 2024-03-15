using Microsoft.EntityFrameworkCore;
using Ultracar.Context;

var builder = WebApplication.CreateBuilder(args);

// add services to the container.
builder.Services.AddControllers();

// Register the database context using dependency injection
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
