namespace Domain
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }= String.Empty;

        public virtual List<Model>? Models { get; set; }
    }
}
