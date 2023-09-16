using MagazineTracking.Infrastructure.EF.DatabaseContext;
using MagazineTracking.WebAPI;



var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
using var scope = app.Services.CreateScope();
AppDbContext appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
startup.Configure(app, builder.Environment, appDbContext);
