using Foundation;
using Applicaton.Cars.Queries.GetCarFilterConfig;

namespace Applicaton.Common.Helpers
{
    public class ApplicationHelper
    {
        public static CarFilterConfigDto CarFilterConfigDto = new()
        {
            CarFilerTypeConfigs = new CarFilerTypeConfig[]
            {

	            new()
	            {
		            Id = Guid.Parse("c31986a3-4f3f-42ff-a94f-2dfe00f9f228"),
		            Name = "Кроссовер",
		            ImageUrl = "images/cars/types/Krossover.png",
		            Query = x=>x.Model.CarType.Name.ToLower() == "кроссовер"
	            },
                new()
                {
                    Id = Guid.Parse("a2f986fe-4438-4497-ab46-60b0d73c70a6"),
                    Name = "Седан",
                    ImageUrl = "images/cars/types/Sedan.png",
                    Query = x=>x.Model.CarType.Name.ToLower() == "седан"
                },
                new()
                {
                    Id = Guid.Parse("d9c638da-1390-4d9c-9c5f-84a968b5d195"),
                    Name = "Хэтчбек",
                    ImageUrl = "images/cars/types/Hetchbek.png",
                    Query = x=>x.Model.CarType.Name.ToLower() == "хэтчбек"
                },
                new()
                {
                    Id = Guid.Parse("006ae0eb-02b7-48ec-b460-ae2ee389d5e3"),
                    Name = "Минивэн",
                    ImageUrl = "images/cars/types/Miniven.png",
                    Query = x=>x.Model.CarType.Name.ToLower() == "минивэн"
                }
            },
            CarFilerPowerReserveConfigs = new CarFilerPowerReserveConfig[]
            {
                new()
                {
                    Id = Guid.Parse("f0e04075-c40d-4cfe-ad77-986ede5aa7ae"),
                    Name = "400-600 км",
                    Query = x=>Convert.ToDouble(x.Car_PropValues.FirstOrDefault(y=>y.PropValue.Property.Name.ToLower().Contains("запас хода"))
                        .PropValue.Value) >= 400
                    &&
                    Convert.ToDouble(x.Car_PropValues.FirstOrDefault(y=>y.PropValue.Property.Name.ToLower().Contains("запас хода"))
                        .PropValue.Value) <= 600
                },
                new()
                {
                    Id = Guid.Parse("dbbd9519-a570-457c-ab3e-90eeb24c53e5"),
                    Name = "600 км и больше",
                    Query = x=>Convert.ToDouble(x.Car_PropValues.FirstOrDefault(y=>y.PropValue.Property.Name.ToLower().Contains("запас хода"))
                                   .PropValue.Value) >= 600
                }
            },
            CarFilerPriceConfigs = new CarFilerPriceConfig[]
            {
                new()
                {
                    Id = Guid.Parse("57c8bfd1-da17-461f-95d6-3335ce394155"),
                    Name = "до 25 000$",
                    MinPrice = 8000,
                    MaxPrice = 25000,
                    Query = x=>x.Price < 25000
                },
                new()
                {
                    Id = Guid.Parse("c050b1f6-e78c-4725-9e7d-19a1e2a1b171"),
                    Name = "от 25 000$ и выше",
                    MinPrice = 25000,
                    MaxPrice = 200000,
                    Query = x=>x.Price >= 25000
                }
            },
            CarFilerManufacturersConfigs = new CarFilerManufacturersConfig[]
            {
                new()
                {
                    Id = Guid.Parse("9f6dcbf6-e56e-42f6-8994-5dfda84552aa"),
                    Name = "Zeekr",
                    Query = x=>x.Model.Name.ToLower().Contains("zeekr")
				},
                new()
                {
	                Id = Guid.Parse("08ffe145-51d8-432f-b0c3-cd5f922491b1"),
	                Name = "Tesla",
	                Query = x=>x.Model.Name.ToLower().Contains("tesla")
				},
                new()
                {
	                Id = Guid.Parse("3907bfff-5f87-496b-be2b-2d6340f6495c"),
	                Name = "BMW",
	                Query = x=>x.Model.Name.ToLower().Contains("bmw")
				},
                new()
                {
	                Id = Guid.Parse("24c13d62-3a52-4111-a0f2-49d5f0d6a7eb"),
	                Name = "LI",
	                Query = x=>x.Model.Name.ToLower().Contains("li")
				}
			},
            CarFilerBatteryCapacityConfigs = new CarFilerBatteryCapacityConfig[]
            {
                new()
                {
                    Id = Guid.Parse("3fadaeae-c9e4-475b-9803-5c80abddcc9b"),
                    Name = "До 50 КВ/ч",
                    Query = x=> Convert.ToDouble(x.Car_PropValues.FirstOrDefault(x=>x.PropValue.Property.Name.ToLower().Contains("мкость батареи"))
                        .PropValue.Value) < 50
                },
                new()
                {
                    Id = Guid.Parse("a3ede7ae-5ab3-408d-b3af-91ead9c03646"),
                    Name = "От 50 КВ/ч",
                    Query = x=> Convert.ToDouble(x.Car_PropValues.FirstOrDefault(x=>x.PropValue.Property.Name.ToLower().Contains("мкость батареи"))
                        .PropValue.Value) >= 50
                }
            },

            CarFilerDriveModeConfigs = new CarFilerDriveModeConfig[]
            {
                new()
                {
                    Id = Guid.Parse("1c15a6cc-88c1-45c9-879b-4dbc722b2ade"),
                    Name = "Полный",
                    Query = x=> x.Car_PropValues.FirstOrDefault(x=>x.PropValue.Property.Name.Contains("Привод"))
                        .PropValue.Value.Contains("Полный")
                },
                new()
                {
                    Id = Guid.Parse("c19bd9d0-f558-41e3-816d-f1524c5ad27b"),
                    Name = "Передний",
                    Query = x=> x.Car_PropValues.FirstOrDefault(x=>x.PropValue.Property.Name.Contains("Привод"))
                        .PropValue.Value.Contains("Передний")
                },
                new()
                {
                    Id = Guid.Parse("a3e7aaa6-2c28-49c9-a7a3-4f1e11e186bd"),
                    Name = "Задний",
                    Query = x=> x.Car_PropValues.FirstOrDefault(x=>x.PropValue.Property.Name.Contains("Привод"))
                        .PropValue.Value.Contains("Задний")
                },
            }
        };
    }
}
