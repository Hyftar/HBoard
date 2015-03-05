using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBoard.Chess
{
    public enum CastlingPolicy
    {
        Disabled = 0,
        Short = 1 << 0,
        Long = 1 << 1,
        Both = ~(1 << 2) 
    }
}
