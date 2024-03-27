﻿namespace CarsClient.Models.Dto
{
    public class CarFullInfoDto
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
		public string Description { get; set; }
		public string PageTitle { get; set; }
		public string ModelName { get; set; }
        public string CarType { get; set; }
        public int ProductionYear { get; set; }
        public List<PropertyCategories> Categories { get; set; }
        public List<ImageInfo> Images { get; set; }
		public List<SameCarInfoDto> SameCars { get; set; }
	}
}
