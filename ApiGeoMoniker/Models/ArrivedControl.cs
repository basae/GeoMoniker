using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArrivedControl
    {
        public long Id { get; set; }
        public string Unit { get; set; }
        public string Terminal { get; set; }
        public string AwaitedArrival { get; set; }
        public string ActualArrival { get; set; }
        public int DiffMinutes { get; set; } 

    }
}
