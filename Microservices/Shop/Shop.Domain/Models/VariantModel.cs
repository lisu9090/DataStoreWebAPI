using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    public class VariantModel
    {
        public string Key { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string DeliveredIn { get; set; }
        public string ArticleCode { get; set; }
        public string Color { get; set; }
        //public ColorModel Color { get; set; }
        public int Size { get; set; }
        //public SizeModel Size { get; set; }
    }
}
