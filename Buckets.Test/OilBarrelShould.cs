using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Buckets.Test
{
    public class OilBarrelShould
    {
        [Fact]
        public void HaveDefaultCapacityOf159()
        {
            OilBarrel sut;
            int defaultCapacity = 159;

            sut = new OilBarrel();

            Assert.Equal(defaultCapacity, sut.Capacity);
        }

        [Fact]
        public void HaveDefaultContentOfZero()
        {
            OilBarrel sut;
            int defaultContent = 0;

            sut = new OilBarrel();

            Assert.Equal(defaultContent, sut.Content);
        }

        [Fact]
        public void HaveRequestedContentOf100()
        {
            OilBarrel sut;
            int requestedContent = 100;

            sut = new OilBarrel(requestedContent);

            Assert.Equal(requestedContent, sut.Content);
        }
    }
}
