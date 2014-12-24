using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Point
    {
        public long? Id { get; set; }
        public string Description { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public bool IsStart { get; set; }
        public bool isEnd { get; set; }
        public int Order { get; set; }
    }
}
