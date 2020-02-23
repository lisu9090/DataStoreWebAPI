using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
    public class SizeModel
    {
        public int ID { get; set; }
        public int Size { get; set; }
        public int Q1ID { get; set; }
        public Q1Model Q1 { get; set; }
    }
}
