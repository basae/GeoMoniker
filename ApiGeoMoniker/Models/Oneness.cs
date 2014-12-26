using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Oneness
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long IdCompany { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        
    }
}
