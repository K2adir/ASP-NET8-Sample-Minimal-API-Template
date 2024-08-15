using aspnet_8_api_gamestore.Data;
using aspnet_8_api_gamestore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace aspnet_8_api_gamestore.Endpoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("genres");

            group.MapGet(
                "/",
                async (GameStoreContext context) =>
                    await context
                        .Genres //
                        .Select(genre => genre.ToDto())
                        .AsNoTracking()
                        .ToListAsync()
            );
            return group;
        }
    }
}
