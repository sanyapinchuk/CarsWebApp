﻿namespace CarsClient.Models.Dto
{
    public class CarShortInfo
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string ModelName { get; set; }
        public string CompanyName { get; set; }
        public string TitleImagePath { get; set; }
        public string Color { get; set; }
        public List<PropertyShortInfo> Properties { get; set; }
    }
}
