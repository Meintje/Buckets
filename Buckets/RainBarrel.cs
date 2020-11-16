using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    public class RainBarrel : Container
    {
        public RainBarrel() : base()
        {
            Capacity = 120;
        }

        public RainBarrel(int content) : this()
        {
            Content = content;
        }

        public RainBarrel(int content, int capacity)
        {
            Capacity = SelectBarrelCapacity(capacity);
            Content = content;
        }

        private int SelectBarrelCapacity(int desiredCapacity)
        {
            if (desiredCapacity <= 80)
            {
                return 80;
            }
            else if (desiredCapacity <= 120)
            {
                return 120;
            }
            else
            {
                return 160;
            }
        }
    }
}
