namespace CarsServer.Models
{
    public class PropValue
    {
        public int Id { get; set; }
        public string? Value { get; set; }

        public virtual List<Property_PropertyValue>? Property_PropertyValues { get; set; }
    }
}
