using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto
{
    public class CarInfoShortDto
    {
        public Guid Id { get; set; }
        //public int Price { get; set; }
        public string ModelName { get; set; }
        public string TitleImagePath { get; set; }
        public List<(string propName, string propValue)> properties { get; set; }
    }
}
