using Dometrain.EFCore.API.Data;
using Dometrain.EFCore.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dometrain.EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController(MoviesContext context) : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await context.Movies.ToListAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var movie = await context.Movies
            .Include(x => x.Genre)
            .SingleOrDefaultAsync(x => x.Id == id);

        return movie == null ? NotFound() : Ok(movie);
    }

    [HttpGet("by-year/{year:int}")]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByYear([FromRoute] int year)
    {
        var filteredMovies = await context.Movies.Where(x => x.ReleaseDate.Year == year)
            .ToListAsync();

        return Ok(filteredMovies);
    }

    [HttpGet("until-age/{ageRating}")]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUntilAge([FromRoute] AgeRating ageRating)
    {
        var filteredMovies = await context.Movies.Where(x => x.AgeRating <= ageRating)
            .ToListAsync();

        return Ok(filteredMovies);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Movie movie)
    {
        await context.Movies.AddAsync(movie);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { movie.Id }, movie);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Movie movie)
    {
        var existingMovie = await context.Movies.FindAsync(id);
        if (existingMovie == null)
        {
            return NotFound();
        }
        existingMovie.ChangeMovieInformation(movie.Title, movie.ReleaseDate, movie.Synopsis);
        await context.SaveChangesAsync();

        return Ok(existingMovie);
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var existingMovie = await context.Movies.FindAsync(id);
        if (existingMovie == null)
        {
            return NotFound();
        }

        context.Movies.Remove(existingMovie);
        await context.SaveChangesAsync();

        return NoContent();
    }


}