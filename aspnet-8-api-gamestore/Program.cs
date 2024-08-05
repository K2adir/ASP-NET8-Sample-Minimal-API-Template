using aspnet_8_api_gamestore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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

app.MapGet("games", () => games);

app.MapGet("/", () => "Hello World!");

app.Run();
