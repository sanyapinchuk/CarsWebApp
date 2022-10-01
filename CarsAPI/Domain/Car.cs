namespace Domain
{
    public class Car
    {
        public Guid Id { get; set; }        
        public int Price { get; set; }


        public Guid ModelId { get; set; }
        public virtual Model? Model { get; set; }

        public virtual List<Car_Color>? Car_Colors { get; set; }
        public virtual List<Car_Prop_Value>? Car_Prop_Values { get; set; }

    }
}
 