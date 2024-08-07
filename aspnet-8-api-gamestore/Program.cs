using aspnet_8_api_gamestore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games =
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

// get games
app.MapGet("games", () => games);

// get games/1
app.MapGet(
        "games/{id}",
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
app.MapPost(
    "games",
    (CreateGameDto newGame) =>
    {
        GameDto game =
            new(
                games.Count + 1, //
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
        games.Add(game);

        return Results.CreatedAtRoute( //
            GetGameEndpointName,
            new { id = game.Id },
            game
        );
    }
);
app.MapPut(
    "games/{id}",
    (int id, UpdateGameDto updatedGame) =>
    {
        var index = games.FindIndex(game => game.Id == id);

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

app.MapDelete(
    "games/{id}",
    (int id) =>
    {
        games.RemoveAll(game => game.Id == id);

        return Results.NoContent();
    }
);

// write delete

app.Run();
