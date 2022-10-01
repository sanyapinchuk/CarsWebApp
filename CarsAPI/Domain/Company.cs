namespace Domain
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }= String.Empty;

        public virtual List<Model>? Models { get; set; }
    }
}
