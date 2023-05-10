namespace Domain
{
    public class Model
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public Guid CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public Guid CarTypeId { get; set; }
        public virtual CarType? CarType { get; set; }

        public virtual List<Car>? Cars { get; set; }   
    }
}
