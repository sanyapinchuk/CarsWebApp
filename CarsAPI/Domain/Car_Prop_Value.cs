namespace Domain
{
    public class Car_Prop_Value
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; }
        public virtual Car? Car { get; set; }

        public Guid Prop_ValueId { get; set; }
        public virtual Property_PropertyValue? Prop_Value { get; set; }

    }
}
