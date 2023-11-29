using System.ComponentModel.DataAnnotations;

namespace DiceParadiceApi.Models.DataTransferObjects;

public abstract class BoardGameForManipulationDto
{
    public string Name { get; set; }
    public string Description { get; set; }

    [Range(0, 18, ErrorMessage = "Age Restrictions must be from 0 to 18")]
    public int AgeRestriction { get; set; } = 0;
    [Range(0, Double.MaxValue, ErrorMessage = "Price has to be positive number")]
    public double Price { get; set; }
    public string Category { get; set; } //TODO: Category table
    public string Settings { get; set; } //TODO: Settings table
    public string ImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Author { get; set; }
    public string Language { get; set; }
    public int Availability { get; set; }
}