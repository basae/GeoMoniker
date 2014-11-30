using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class ServiceResponse<T>
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> List { get; set; }
        public object ObjectResult { get; set; }

        public ServiceResponse()
        {
            Error = false;
            Message = string.Empty;
            List = null;
        }
    }

    
}
