using Buckets.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Buckets
{
    public class Bucket : Container
    {
        public Bucket() : base()
        {
            Capacity = 12;
        }

        public Bucket(int content) : this()
        {
            Content = content;
        }

        public Bucket(int content, int capacity)
        {
            var minimumCapacity = 10;

            if (capacity > minimumCapacity)
            {
                Capacity = capacity;
            }
            else
            {
                Capacity = minimumCapacity;
            }

            Content = content;
        }

        public void Fill(Bucket bucket)
        {
            if (this == bucket) { throw new SameBucketException("It is not possible to fill a bucket with itself."); }
            else
            {
                base.Fill(bucket.Content);

                // Empty the bucket that was used to fill this one with the amount that was transferred.
                bucket.Empty(addedAmount);
            }          
        }
    }
}
