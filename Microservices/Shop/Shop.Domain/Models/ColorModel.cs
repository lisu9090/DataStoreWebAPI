using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    public class ColorModel
    {
        public int ID { get; set; }
        public string Color { get; set; }
        public int ColorCodeID { get; set; }
        public ColorCodeModel ColorCode { get; set; }
    }
}
