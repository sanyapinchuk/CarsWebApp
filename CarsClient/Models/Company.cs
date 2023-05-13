namespace CarsClient.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual List<Model>? Models { get; set; }
    }
}
