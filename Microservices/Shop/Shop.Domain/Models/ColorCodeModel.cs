using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    public class ColorCodeModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public virtual ICollection<ColorModel> Colors { get; set; }
    }
}
