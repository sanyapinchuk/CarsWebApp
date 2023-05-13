namespace CarsClient.Models
{
    public class Car_Prop_Value
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public virtual Car? Car { get; set; }
        public int Prop_ValueId { get; set; }
        public virtual Property_PropertyValue? Prop_Value { get; set; }

    }
}
