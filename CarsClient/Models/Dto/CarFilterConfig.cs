
namespace CarsClient.Models.Dto
{
    public class CarFilterConfig
    {
        public CarFilerTypeConfig[] CarFilerTypeConfigs { get; set; }

        public CarFilerPriceConfig[] CarFilerPriceConfigs { get; set; }

        public CarFilerPowerReserveConfig[] CarFilerPowerReserveConfigs { get; set; }

        public CarFilerManufacturersConfig[] CarFilerManufacturersConfigs { get; set; }

        public CarFilerBatteryCapacityConfig[] CarFilerBatteryCapacityConfigs { get; set; }

        public CarFilerDriveModeConfig[] CarFilerDriveModeConfigs { get; set; }
    }

    public class CarFilerTypeConfig
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }

    public class CarFilerPriceConfig
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }
    }

    public class CarFilerPowerReserveConfig
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class CarFilerManufacturersConfig
    {
	    public Guid Id { get; set; }

	    public string Name { get; set; }
    }

    public class CarFilerBatteryCapacityConfig
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class CarFilerDriveModeConfig
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
