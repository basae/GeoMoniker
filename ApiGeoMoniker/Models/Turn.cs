using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Turn
    {
        public long? Id { get; set; }
        public long? IdRoute { get; set; }
        public long? IdPoint { get; set; }
        public DateTime AwaitedArrival { get; set; }

    }
}
