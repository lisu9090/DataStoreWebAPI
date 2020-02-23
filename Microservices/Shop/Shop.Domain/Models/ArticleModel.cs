using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    public class ArticleModel
    {
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Q1 { get; set; }
        public string ColorCode { get; set; }
        public virtual ICollection<VariantModel> Variants { get; set; }
    }
}
