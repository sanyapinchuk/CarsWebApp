namespace CarsServer.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual List<Property_PropertyValue>? Property_PropertyValues { get; set; }
    }
}
