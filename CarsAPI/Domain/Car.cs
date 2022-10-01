namespace Domain
{
    public class Car
    {
        public Guid Id { get; set; }        
        public int Price { get; set; }


        public Guid ModelId { get; set; }
        public virtual Model? Model { get; set; }

        public virtual List<Car_Color>? Car_Colors { get; set; }
        public virtual List<Car__Property_PropValue>? Car_Prop_Values { get; set; }
        public virtual List<Image>? Images { get; set; }

    }
}
 