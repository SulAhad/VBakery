using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBakery.Model
{
    public class PriceForMenu
    {
        public int Id { get; set; }
        public string PriceFruct      { get; set; }
        public string PriceMan        { get; set; }
        public string PriceShoko      { get; set; }
        public string PriceMussoviy   { get; set; }
        public string PriceSvadebniy  { get; set; }
        public string PriceBirthday   { get; set; }
        public string PriceKids { get; set; }
    }
}
