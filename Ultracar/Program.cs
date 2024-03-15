using Microsoft.EntityFrameworkCore;
using Ultracar.Context;
using Ultracar.Repository;

var builder = WebApplication.CreateBuilder(args);

// add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUltracarDbContext, UltracarDbContext>();
builder.Services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();

// register database connection string
builder.Services.AddDbContext<UltracarDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

var app = builder.Build();

// configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// set default route
app.MapGet("/", () => "[ultracar] - api initialized");

// set up service provider so we can Seed info into the DB.
var serviceProvider = app.Services;
var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UltracarDbContext>();

// initialize database seeder class
Seeder.Initialize(context);

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
