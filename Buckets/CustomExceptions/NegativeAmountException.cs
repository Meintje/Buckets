using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets.CustomExceptions
{
    public class NegativeAmountException : ArgumentException
    {
        public NegativeAmountException(string message) : base(message)
        {
        }
    }
}
