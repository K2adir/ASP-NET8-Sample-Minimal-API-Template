using aspnet_8_api_gamestore.Data;
using aspnet_8_api_gamestore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

// extends app and migrates db
await app.MigrateDbAsync();

app.Run();
