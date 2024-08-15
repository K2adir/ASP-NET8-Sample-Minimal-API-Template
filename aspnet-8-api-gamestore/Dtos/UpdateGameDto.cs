using System.ComponentModel.DataAnnotations;

namespace aspnet_8_api_gamestore.Dtos;

public record class UpdateGameDto(
    [Required] [StringLength(50)] string Name, //
    [Required] int GenreId,
    [Required] [Range(1, 1000)] decimal Price,
    [Required] DateOnly ReleaseDate
);
