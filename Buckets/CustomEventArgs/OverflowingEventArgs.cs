using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets.CustomEventArgs
{
    public class OverflowingEventArgs : EventArgs
    {
        public OverflowingEventArgs(int amountThatWillBeSpilled, int amountThatCanBeAdded)
        {
            AmountThatWillBeSpilled = amountThatWillBeSpilled;
            AmountThatCanBeAdded = amountThatCanBeAdded;
            Response = OverflowingEventResponse.IgnoreOverflow;
            AmountToBeAdded = 0;
        }

        public int AmountThatWillBeSpilled { get; private set; }
        public int AmountThatCanBeAdded { get; private set; }
        public OverflowingEventResponse Response { get; set; }
        public int AmountToBeAdded { get; set; }
    }
}
