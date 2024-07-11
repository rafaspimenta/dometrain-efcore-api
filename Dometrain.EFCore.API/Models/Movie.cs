using System;

namespace Dometrain.EFCore.API.Models;

public class Movie
{
    private Movie() { }

    public Movie(string? title, DateTime releaseDate, string? synopsis)
    {
        Title = title;
        ReleaseDate = releaseDate;
        Synopsis = synopsis;
    }

    public int Id { get; private set; }
    public string? Title { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string? Synopsis { get; private set; }

    public void UpdateMovieDetails(string? title, DateTime releaseDate, string? synopsis)
    {
        Title = title;
        ReleaseDate = releaseDate;
        Synopsis = synopsis;
    }


}