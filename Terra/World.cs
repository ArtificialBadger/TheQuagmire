using System;
using System.Collections.Generic;
using System.Text;

namespace Terra
{
    public class World
    {
        public String Name { get; set; }

        public decimal Population { get; set; }

        public List<Partition> Continents { get; set; }
    }
}
