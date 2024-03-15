using Microsoft.EntityFrameworkCore;
using Ultracar.Repository;

var builder = WebApplication.CreateBuilder(args);

// add services to the container.
builder.Services.AddControllers();

// register the database context using dependency injection
builder.Services.AddDbContext<UltracarContext>(options =>
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
