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
        public ArticleModel Article { get; set; }
        public int ColorID { get; set; }
        public ColorModel Color { get; set; }
        public int SizeID { get; set; }
        public SizeModel Size { get; set; }
    }
}
