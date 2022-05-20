﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synger.Spotify
{
    public interface ISpotifySongResolver
    {
        string GetClientId();

        Task Authorize();
    }
}
