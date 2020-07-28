using System;
using System.Collections.Generic;
using System.Text;
using Terra.Agolora.Continents;

namespace Terra.Agolora
{
    public static class AgoloraWorld
    {
        public static World Agolora = new World() { Population = 7_200_000_000, Name = "Agolora", Continents =  AgoloraPartitions.Continents };
    }
}
