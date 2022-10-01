namespace Domain
{
    public class Car__Property_PropValue
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; }
        public virtual Car? Car { get; set; }

        public Guid Property_PropValueId { get; set; }
        public virtual Property_PropValue? Prop_Value { get; set; }

    }
}
