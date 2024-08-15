using aspnet_8_api_gamestore.Data;
using aspnet_8_api_gamestore.Dtos;
using aspnet_8_api_gamestore.Entities;
using aspnet_8_api_gamestore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace aspnet_8_api_gamestore.Endpoints;

public static class GamesEndpoints
{ //
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        //.WithPV  makes data validation from DTO to be effective
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet(
            "/",
            async (GameStoreContext context) =>
                await context
                    // we need .Include because otherwise we can't pull genre out of DTO
                    .Games.Include(game => game.Genre)
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking()
                    .ToListAsync()
        );

        group
            .MapGet(
                "/{id}",
                async (int id, GameStoreContext dbContext) =>
                {
                    Game? game = await dbContext.Games.FindAsync(id);

                    return game is null
                        ? //
                        Results.NotFound()
                        : Results.Ok(game);
                }
            )
            .WithName(GetGameEndpointName);

        // Post /games
        group.MapPost(
            "/",
            async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = newGame.ToEntity();
                game.Genre = await dbContext.Genres.FindAsync(newGame.GenreId);
                //
                //
                await dbContext.Games.AddAsync(game);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute( //
                    GetGameEndpointName,
                    new { id = game.Id },
                    game.ToGameDetailsDto()
                );
            }
        );
        //
        group.MapPut(
            "/{id}",
            async (int id, UpdateGameDto updatedGame, GameStoreContext context) =>
            {
                var existingGame = await context.Games.FindAsync(id);
                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                context.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));

                await context.SaveChangesAsync();
                return Results.NoContent();
            }
        );

        group.MapDelete(
            "/{id}",
            async (int id, GameStoreContext context) =>
            {
                await context.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
                return Results.NoContent();
            }
        );
        return group;
    }
}
