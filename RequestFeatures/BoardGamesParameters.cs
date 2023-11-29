namespace DiceParadiceApi.Models.RequestFeatures;

public class BoardGamesParameters : RequestParameters
{
    public double MinPrice { get; set; } = .0;
    public double MaxPrice { get; set; } = double.MaxValue;
    public string SearchTerm { get; set; }
    public bool ValidAgeRange => MaxPrice > MinPrice;

    public BoardGamesParameters()
    {
        OrderBy = "Name";
    }
}