namespace Domain
{
    public class Property: BaseEntity
    {
        public string Name { get; set; }= String.Empty;
        public bool IsKeyProperty { get; set; }

        public virtual List<PropValue>? PropValues { get; set; }
    }
}
