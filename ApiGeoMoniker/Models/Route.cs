using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Route
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Point> Points { get; set; }
        public long User { get; set; }
        public long IdCompany { get; set; }
    }

    public class RouteOuput
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string UserInsert { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Company { get; set; }
    }
}
