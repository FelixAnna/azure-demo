using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Felix.Azure.MvcMovie.Models
{
    public class ActorViewModel
    {
        public int ID { get; set; }
        
        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string Description { get; set; }
    }
}
