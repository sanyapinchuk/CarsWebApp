using Shared.Dto;

namespace Applicaton.Cars.Queries.GetCarsList
{
    public class CarListDto
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string ModelName { get; set; }
        public string TitleImagePath { get; set; }
        public List<PropertyDto> Properties { get; set; }
    }
}
