using aspnet_8_api_gamestore.Data;
using aspnet_8_api_gamestore.Dtos;
using aspnet_8_api_gamestore.Entities;

namespace aspnet_8_api_gamestore.Endpoints;

public static class GamesEndpoints
{ //
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games =
    [
        new(
            1, //
            "Street fighter",
            "Fighting",
            19.99M,
            new DateOnly(1993, 1, 1)
        ),
        new(
            2, //
            "fighter",
            "dude",
            29.99M,
            new DateOnly(1995, 1, 1)
        ),
        new(
            2, //
            "fighter",
            "dude",
            29.99M,
            new DateOnly(1995, 1, 1)
        ),
        new(
            3, //
            "garden",
            "dudedude",
            39.99M,
            new DateOnly(1997, 1, 1)
        ),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        //.WithPV  makes data validation from DTO to be effective
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);

        // get games/1
        group
            .MapGet(
                "/{id}",
                (int id) =>
                {
                    GameDto? game = games.Find(game => game.Id == id);

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
            (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game =
                    new()
                    { //
                        Name = newGame.Name,
                        Genre = dbContext.Genres.Find(newGame.GenreId),
                        GenreId = newGame.GenreId,
                        Price = newGame.Price,
                        ReleaseDate = newGame.ReleaseDate
                    };
                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                GameDto gameDto =
                    new(
                        game.Id, //`
                        game.Name,
                        game.Genre!.Name,
                        game.Price,
                        game.ReleaseDate
                    );

                return Results.CreatedAtRoute( //
                    GetGameEndpointName,
                    new { id = game.Id },
                    gameDto
                );
            }
        );
        //
        group.MapPut(
            "/{id}",
            (int id, UpdateGameDto updatedGame) =>
            {
                var index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }

                games[index] = new GameDto(
                    id, //
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                );

                return Results.NoContent();
            }
        );

        group.MapDelete(
            "/{id}",
            (int id) =>
            {
                games.RemoveAll(game => game.Id == id);

                return Results.NoContent();
            }
        );
        return group;
    }
}
