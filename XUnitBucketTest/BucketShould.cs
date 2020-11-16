using System;
using Buckets;
using Xunit;

namespace XUnitBucketTest
{
    public class BucketShould
    {
        [Fact]
        public void HaveDefaultCapacityOfTwelve()
        {
            Bucket sut;
            int defaultCapacity = 12;

            sut = new Bucket(4);

            Assert.Equal(sut.Capacity, defaultCapacity);
        }

        [Fact]
        public void HaveDefaultContentOfZero()
        {
            Bucket sut;
            int defaultContent = 0;

            sut = new Bucket();

            Assert.Equal(sut.Content, defaultContent);
        }

        [Fact]
        public void HaveContentOfFour()
        {
            Bucket sut;
            int desiredContent = 4;

            sut = new Bucket(4);

            Assert.Equal(sut.Content, desiredContent);
        }

        [Fact]
        public void PublishFullEvent()
        {
            bool eventFullIsTriggered = false;
            Bucket sut = new Bucket();

            sut.FullEventHandler += (o, e) => { eventFullIsTriggered = true; };
            sut.Fill(12);

            Assert.True(eventFullIsTriggered);
        }
    }
}
