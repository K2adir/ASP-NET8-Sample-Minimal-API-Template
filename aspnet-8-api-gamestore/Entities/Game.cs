namespace aspnet_8_api_gamestore.Entities;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }

    //
    public required int GenreId { get; set; }
    public Genre? Genre { get; set; }

    //
    public required decimal Price { get; set; }
    public required DateOnly ReleaseDate { get; set; }
}
