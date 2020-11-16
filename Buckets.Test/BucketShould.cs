using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Buckets.Test
{
    public class BucketShould
    {
        [Fact]
        public void HaveDefaultCapacityOfTwelve()
        {
            Bucket sut;
            int defaultBucketCapacity = 12;

            sut = new Bucket(4);

            Assert.Equal(defaultBucketCapacity, sut.Capacity);
        }

        [Fact]
        public void HaveDefaultContentOfZero()
        {
            Bucket sut;
            int defaultContent = 0;

            sut = new Bucket();

            Assert.Equal(defaultContent, sut.Content);
        }

        [Fact]
        public void HaveContentOfFour()
        {
            Bucket sut;
            int desiredContent = 4;

            sut = new Bucket(4);

            Assert.Equal(desiredContent, sut.Content);
        }

        [Fact]
        public void HaveMinimumCapacityOfTen()
        {
            Bucket sut;
            int minimumCapacity = 10;

            sut = new Bucket(0, 4);

            Assert.Equal(minimumCapacity, sut.Capacity);
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

        [Fact]
        public void PublishOverflowEvent()
        {
            bool overflowEventIsTriggered = false;
            Bucket sut = new Bucket();

            sut.OverflowedEventHandler += (o, e) => { overflowEventIsTriggered = true; };
            sut.Fill(99);

            Assert.True(overflowEventIsTriggered);
        }
    }
}
