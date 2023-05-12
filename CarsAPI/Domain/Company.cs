namespace Domain
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Model>? Models { get; set; }
    }
}
