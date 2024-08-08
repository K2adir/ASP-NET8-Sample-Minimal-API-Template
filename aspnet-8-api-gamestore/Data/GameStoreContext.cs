using aspnet_8_api_gamestore.Entities;
using Microsoft.EntityFrameworkCore;

namespace aspnet_8_api_gamestore.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();
    
}
