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
		            Id = Guid.Parse("3122e75f-53c1-4447-8187-0f6cfbd97453"),
		            Name = "Volkswagen",
		            Query = x=>x.Model.Name.ToLower().Contains("volkswagen")
	            },
	            new()
	            {
		            Id = Guid.Parse("f48eb000-6389-4c61-bf90-245d7c584c66"),
		            Name = "Xiaomi",
		            Query = x=>x.Model.Name.ToLower().Contains("xiaomi")
	            },
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
				},
                new()
                {
	                Id = Guid.Parse("d4eb4c50-d31c-49aa-8f27-15c24f1caeb9"),
	                Name = "Arcfox",
	                Query = x=>x.Model.Name.ToLower().Contains("arcfox")
                },
                new()
                {
	                Id = Guid.Parse("1d97c633-4a4a-4390-9ce7-166a25628c80"),
	                Name = "Avatr",
	                Query = x=>x.Model.Name.ToLower().Contains("avatr")
                },
                new()
                {
	                Id = Guid.Parse("6c0220d2-8af2-40ad-9dca-89366af285b9"),
	                Name = "Xpeng",
	                Query = x=>x.Model.Name.ToLower().Contains("xpeng")
                },
                new()
                {
	                Id = Guid.Parse("6bcf0344-98bf-4bc3-9047-24e138239eed"),
	                Name = "Tank",
	                Query = x=>x.Model.Name.ToLower().Contains("tank")
                },
                new()
                {
	                Id = Guid.Parse("40263463-ea59-4fc2-99fa-5db380f18042"),
	                Name = "Bestune",
	                Query = x=>x.Model.Name.ToLower().Contains("bestune")
                },
                new()
                {
	                Id = Guid.Parse("157ec26b-6fed-4e25-8a9f-c6ac4784b814"),
	                Name = "IM Motors",
	                Query = x=>x.Model.Name.ToLower().Contains("im motor")
                },
                new()
                {
	                Id = Guid.Parse("9b90f3b3-8fad-425e-af88-b826df8aff9c"),
	                Name = "GAC",
	                Query = x=>x.Model.Name.ToLower().Contains("gac")
                },
                new()
                {
	                Id = Guid.Parse("d26b1771-45f4-4a5d-bdff-ea47dd8e3a81"),
	                Name = "KIA",
	                Query = x=>x.Model.Name.ToLower().Contains("kia")
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
