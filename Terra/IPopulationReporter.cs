using System;
using System.Collections.Generic;
using System.Text;

namespace Terra
{
    public interface IPopulationReporter
    {
        string Report(decimal total, IList<Challenge> challenges);
    }
}
