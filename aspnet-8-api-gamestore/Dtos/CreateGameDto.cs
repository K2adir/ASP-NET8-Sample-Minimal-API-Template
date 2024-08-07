namespace aspnet_8_api_gamestore.Dtos;

public record class CreateGameDto(
    string Name, //
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
