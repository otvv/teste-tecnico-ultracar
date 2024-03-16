using Microsoft.EntityFrameworkCore;
using Ultracar;
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

// set default route status
app.MapGet("/", () => "[Ultracar] - API Running.");

// set up service provider so that info can be seeded into the DB.
var serviceProvider = app.Services;
var scope = serviceProvider.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<UltracarDbContext>();

// configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();

	// initialize database seeder (only in dev environments)
	Seeder.Initialize(context);
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
