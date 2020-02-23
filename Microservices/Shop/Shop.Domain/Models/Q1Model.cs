using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    public class Q1Model
    {
        public int ID { get; set; }
        public string Q1 { get; set; }
        public virtual ICollection<SizeModel> Sizes { get; set; }
    }
}
