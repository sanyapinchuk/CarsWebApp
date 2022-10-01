namespace Domain
{
    public class Car_Color
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; }
        public virtual Car? Car { get; set; }

        public Guid ColorId { get; set; }
        public virtual Color? Color { get; set; }
    }
}
