namespace CarsClient.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string? Name { get; set; }


        public int ComponyId { get; set; }
        public virtual Company? Company { get; set; }

        public virtual List<Car>? Cars { get; set; }
    }
}
