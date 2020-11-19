using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets.CustomExceptions
{
    // TODO: Think of a better name for this.
    public class SameBucketException : ArgumentException
    {
        public SameBucketException(string message) : base(message)
        {
        }
    }
}
