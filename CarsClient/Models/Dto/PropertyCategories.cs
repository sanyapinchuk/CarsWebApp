namespace CarsClient.Models.Dto;

public class PropertyCategories
{
    public string Category { get; set; }

    public int Priority { get; set; }

    public List<PropertyFullInfo> Properties { get; set; }
}
