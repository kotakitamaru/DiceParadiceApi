using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;

namespace DiceParadiceApi.Models.DataTransferObjects;

public class BoardGameDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public int AgeRestriction { get; set; }
    public double Price { get; set; }
    public string Category { get; set; } //TODO: Category table
    public string Settings { get; set; } //TODO: Settings table
    public string ImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Author { get; set; }
    public string Language { get; set; }
    public int Availability { get; set; }
}