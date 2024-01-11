namespace CarsClient.Models.Dto
{
    public class PropertyFullInfoApi
    {
        public string Property { get; set; }
        public bool IsKeyProperty { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }
    }
}
