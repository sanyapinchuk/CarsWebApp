namespace Domain
{
    public class Property: BaseEntity
    {
        public string Name { get; set; }= String.Empty;
        public bool IsKeyProperty { get; set; }

        public Guid PropCategoryId { get; set; }
        public virtual PropCategory? PropCategory { get; set; }

        public virtual List<PropValue>? PropValues { get; set; }
    }
}
