using System;
using System.Collections.Generic;
using System.Text;

namespace Terra
{
    public struct Challenge
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal PassRate { get; set; }

        public bool Lethal { get; set; }
    }
}
