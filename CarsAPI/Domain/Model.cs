namespace Domain
{
    public class Model
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;


        public Guid ComponyId { get; set; }
        public virtual Company? Company { get; set; }

        public virtual Car? Car { get; set; }
    }
}
