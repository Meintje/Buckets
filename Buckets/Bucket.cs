using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Buckets
{
    public class Bucket : Container
    {
        // TODO: Add constructor chaining to prevent double code
        public Bucket() : base()
        {
            
        }

        public Bucket(int content) : base(content)
        {
            
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
            base.Fill(bucket.Content);

            bucket.Empty();
        }
    }
}
