using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets.CustomEventArgs
{
    public class OverflowedEventArgs : EventArgs
    {
        public int SpilledAmount { get; protected set; }

        public OverflowedEventArgs(int spilledAmount)
        {
            SpilledAmount = spilledAmount;
        }
    }
}
