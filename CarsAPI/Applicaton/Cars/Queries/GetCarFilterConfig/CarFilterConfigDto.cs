using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Domain;

namespace Applicaton.Cars.Queries.GetCarFilterConfig
{
    public class CarFilterConfigDto
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

        [JsonIgnore]
        public Expression<Func<Car, bool>> Query { get; set; }
    }

    public class CarFilerPriceConfig
    {
	    public Guid Id { get; set; }

        public string Name { get; set; }

        public int MinPrice { get; set; }
        
        public int MaxPrice { get; set; }

        [JsonIgnore]
        public Expression<Func<Car, bool>> Query { get; set; }
    }

    public class CarFilerPowerReserveConfig
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public Expression<Func<Car, bool>> Query { get; set; }
    }

    public class CarFilerManufacturersConfig
    {
	    public Guid Id { get; set; }

	    public string Name { get; set; }

	    [JsonIgnore]
	    public Expression<Func<Car, bool>> Query { get; set; }
    }

    public class CarFilerBatteryCapacityConfig
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public Expression<Func<Car, bool>> Query { get; set; }
    }
    
    public class CarFilerDriveModeConfig
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public Expression<Func<Car, bool>> Query { get; set; }
    }
}
