using Shared.Dto;

namespace Applicaton.Cars.Queries.GetCarFullInfo
{
    public class CarFullInfoDto
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string PageDescription { get; set; }
        public string PageTitle { get; set; }
        public string ModelName { get; set; }
        public string CarType { get; set; }
        public int ProductionYear { get; set; }
        public List<FullPropertyDto> Properties { get; set; }
        public List<ImageInfoDto> Images { get; set; }
        public List<SameCarInfoDto> SameCars { get; set; }
    }
}
