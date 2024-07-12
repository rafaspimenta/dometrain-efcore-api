using System.Collections.Generic;

namespace Dometrain.EFCore.API.Models;

public class Genre
{
    private Genre() { }

    public Genre(string name, int id = 0)
    {
        Name = name;
        Id = id;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<Movie> Movies { get; private set; } = [];

    public void ChangeName(string name)
    {
        Name = name;
    }
}
