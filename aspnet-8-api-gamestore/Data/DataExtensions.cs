using aspnet_8_api_gamestore.Entities;
using Microsoft.EntityFrameworkCore;

namespace aspnet_8_api_gamestore.Data;

public static class DataExtensions
{
    // auto database creating / migration and update
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        await dbContext.Database.MigrateAsync();
    }
}
