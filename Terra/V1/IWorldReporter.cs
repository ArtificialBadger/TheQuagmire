using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.V1
{
    public interface IWorldReporter
    {
        string Report(World world, ReportVerbosity verbosity);
    }
}
