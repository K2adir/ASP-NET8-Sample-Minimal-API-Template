namespace aspnet_8_api_gamestore.Dtos;

public record class GameDto(
    int Id, //
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
