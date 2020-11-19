using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Buckets.Test
{
    public class RainBarrelShould
    {
        [Fact]
        public void HaveDefaultCapacityOf120()
        {
            RainBarrel sut;
            int defaultCapacity = 120;

            sut = new RainBarrel();

            Assert.Equal(defaultCapacity, sut.Capacity);
        }

        [Fact]
        public void HaveDefaultContentOfZero()
        {
            RainBarrel sut;
            int defaultContent = 0;

            sut = new RainBarrel();

            Assert.Equal(defaultContent, sut.Content);
        }

        [Fact]
        public void HaveRequestedContentOf100()
        {
            RainBarrel sut;
            int requestedContent = 100;

            sut = new RainBarrel(requestedContent);

            Assert.Equal(requestedContent, sut.Content);
        }

        [Fact]
        public void HaveRequestedCapacityOf80()
        {
            RainBarrel sut;
            int requestedCapacity = 80;

            sut = new RainBarrel(0, 79);

            Assert.Equal(requestedCapacity, sut.Capacity);
        }

        [Fact]
        public void HaveRequestedCapacityOf120()
        {
            RainBarrel sut;
            int requestedCapacity = 120;

            sut = new RainBarrel(0, 81);

            Assert.Equal(requestedCapacity, sut.Capacity);
        }

        [Fact]
        public void HaveRequestedCapacityOf160()
        {
            RainBarrel sut;
            int requestedCapacity = 160;

            sut = new RainBarrel(0, 121);

            Assert.Equal(requestedCapacity, sut.Capacity);
        }
    }
}
