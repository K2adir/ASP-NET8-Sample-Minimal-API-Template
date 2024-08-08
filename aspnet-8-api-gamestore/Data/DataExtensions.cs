using aspnet_8_api_gamestore.Entities;
using Microsoft.EntityFrameworkCore;

namespace aspnet_8_api_gamestore.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();
    }
}
