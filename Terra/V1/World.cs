using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.V1
{
    public class World
    {
        public string Name { get; set; }

        public decimal Population { get; set; }

        public List<Partition> Continents { get; set; }
    }
}
