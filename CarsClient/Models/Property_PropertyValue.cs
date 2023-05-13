namespace CarsClient.Models
{
    public class Property_PropertyValue
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }
        public virtual Property? Property { get; set; }
        public int PropValueId { get; set; }
        public virtual PropValue? PropValue { get; set; }

        public virtual List<Car_Prop_Value>? Car_Prop_Values { get; set; }

    }
}
