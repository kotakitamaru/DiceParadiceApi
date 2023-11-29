using System.ComponentModel.DataAnnotations;

namespace DiceParadiceApi.Models;

public class BoardGame
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    [Range(0, 18, ErrorMessage = "Age Restrictions must be from 0 to 18")]
    public int AgeRestriction { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Price has to be positive number")]
    public double Price { get; set; }
    public string Category { get; set; }
    public string Settings { get; set; } 
    public string ImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Author { get; set; }
    public string Language { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Availability has to be positive number")]
    public int Availability { get; set; }
}