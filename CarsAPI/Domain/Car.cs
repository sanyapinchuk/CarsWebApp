namespace CarsServer.Models
{
    public class Car
    {
        public int Id { get; set; }        
        public int Price { get; set; }


        public int ModelId { get; set; }
        public virtual Model? Model { get; set; }

        public virtual List<Car_Color>? Car_Colors { get; set; }
        public virtual List<Car_Prop_Value>? Car_Prop_Values { get; set; }

    }
}
 