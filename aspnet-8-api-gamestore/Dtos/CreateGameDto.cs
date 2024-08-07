using System.ComponentModel.DataAnnotations;

namespace aspnet_8_api_gamestore.Dtos;

public record class CreateGameDto(
    [Required] [StringLength(50)] string Name, //
    [Required] [StringLength(20)] string Genre,
    [Required] [Range(1, 1000)] decimal Price,
    [Required] DateOnly ReleaseDate
);
