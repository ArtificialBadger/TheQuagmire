using System;
using System.Collections.Generic;
using System.Text;
using Terra.V1.Agolora.Continents;
using Terra.V1;

namespace Terra.V1.Agolora
{
    public static class AgoloraWorld
    {
        public static World Agolora = new World() { Population = 7_200_000_000, Name = "Agolora", Continents =  AgoloraPartitions.Continents };
    }
}
