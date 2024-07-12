using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dometrain.EFCore.API.Models;

public class Movie
{
    private Movie() { }

    public Movie(string title, DateTime releaseDate, string? synopsis, int mainGenreId, AgeRating ageRating, int id = 0)
    {
        Title = title;
        ReleaseDate = releaseDate;
        Synopsis = synopsis;
        MainGenreId = mainGenreId;
        Id = id;
        AgeRating = ageRating;
    }

    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public DateTime ReleaseDate { get; private set; }
    public string? Synopsis { get; private set; }
    [JsonIgnore]
    public Genre? Genre { get; private set; }
    public int MainGenreId { get; private set; }
    public AgeRating AgeRating { get; private set; }

    public void ChangeMovieInformation(string title, DateTime releaseDate, string? synopsis)
    {
        Title = title;
        ReleaseDate = releaseDate;
        Synopsis = synopsis;
    }
}

public enum AgeRating
{
    All = 0,
    ElementarySchool = 6,
    HighSchool = 12,
    Adolescent = 16,
    Adult = 18
}