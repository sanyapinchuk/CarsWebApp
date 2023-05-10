namespace Domain
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Name { get; set; }= String.Empty;
        public bool IsKeyProperty { get; set; }

        public virtual List<PropValue>? PropValues { get; set; }
    }
}
