namespace CarsClient.Models.Dto
{
    public class CarFullInfo
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
		public string Description { get; set; }
		public string ModelName { get; set; }
        public string CarType { get; set; }
        public int ProductionYear { get; set; }
        public string CompanyName { get; set; }
        public string Color { get; set; }
        public List<PropertyFullInfo> Properties { get; set; }
        public List<ImageInfo> Images { get; set; }
		public List<SameCarInfoDto> SameCars { get; set; }
	}
}
