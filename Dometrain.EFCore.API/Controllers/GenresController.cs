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
public class GenreController(MoviesContext context) : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(List<Genre>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await context.Genres.ToListAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var genre = await context.Genres.FindAsync(id);
        return genre == null ? NotFound() : Ok(genre);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Genre genre)
    {
        await context.Genres.AddAsync(genre);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { genre.Id }, genre);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Genre genre)
    {
        var existingGenre = await context.Genres.FindAsync(id);
        if (existingGenre == null)
        {
            return NotFound();
        }
        existingGenre.ChangeName(genre.Name);
        await context.SaveChangesAsync();

        return Ok(existingGenre);
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var existingGenre = await context.Genres.FindAsync(id);
        if (existingGenre == null)
        {
            return NotFound();
        }

        context.Genres.Remove(existingGenre);
        await context.SaveChangesAsync();

        return NoContent();
    }


}