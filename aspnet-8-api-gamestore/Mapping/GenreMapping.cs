using aspnet_8_api_gamestore.Dtos;
using aspnet_8_api_gamestore.Entities;

namespace aspnet_8_api_gamestore.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
