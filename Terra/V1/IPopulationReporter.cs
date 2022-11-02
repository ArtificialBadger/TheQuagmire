using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.V1
{
    public interface IPopulationReporter
    {
        string Report(decimal total, IList<Challenge> challenges);
    }
}
