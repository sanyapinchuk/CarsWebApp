namespace CarsClient.Models.Dto
{
    public class SelectedFilterConfig
    {
        public Guid[]? Manufactures { get; set; }

        public Guid[]? Types { get; set; }

        public Guid[]? PowerReserves { get; set; }

        public Guid[]? BatteryCapacity { get; set; }

        public Guid[]? DriveModes { get; set; }

        public int FilterPriceMin { get; set; }

        public int FilterPriceMax { get; set; }
    }
}
