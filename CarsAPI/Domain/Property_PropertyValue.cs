namespace Domain
{
    public class Property_PropertyValue
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }
        public virtual Property? Property { get; set; }

        public Guid PropValueId { get; set; }
        public virtual PropValue? PropValue { get; set; }


        public virtual List<Car_Prop_Value>? Car_Prop_Values { get; set; }

    }
}
