using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets.CustomEventArgs
{
    public enum OverflowingEventResponse
    {
        Cancel,
        IgnoreOverflow,
        FillPartially,
        FillToBrim
    }
}
