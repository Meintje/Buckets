using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    public class OilBarrel : Container
    {
        public OilBarrel() : base()
        {
            Capacity = 159;
        }

        public OilBarrel(int content) : this()
        {
            Content = content;
        }
    }
}
