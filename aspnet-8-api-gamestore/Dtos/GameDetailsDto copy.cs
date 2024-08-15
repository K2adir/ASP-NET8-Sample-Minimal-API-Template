namespace aspnet_8_api_gamestore.Dtos;

public record class GameDetailsDto(
    int Id, //
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);
