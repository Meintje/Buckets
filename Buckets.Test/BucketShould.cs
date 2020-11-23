using Buckets.CustomEventArgs;
using Buckets.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Buckets.Test
{
    public class BucketShould
    {
        #region Constructor
        [Fact]
        public void HaveDefaultCapacityOfTwelve()
        {
            Bucket sut;
            int defaultCapacity = 12;

            sut = new Bucket();

            Assert.Equal(defaultCapacity, sut.Capacity);
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
        public void HaveRequestedContentOfFour()
        {
            Bucket sut;
            int requestedContent = 4;

            sut = new Bucket(4);

            Assert.Equal(requestedContent, sut.Content);
        }

        [Fact]
        public void HaveMinimumCapacityOfTen()
        {
            Bucket sut;
            int minimumCapacity = 10;

            sut = new Bucket(0, 4);

            Assert.Equal(minimumCapacity, sut.Capacity);
        }
        #endregion

        #region Content setter
        #endregion

        #region Fill method
        #endregion

        #region Events
        [Fact]
        public void PublishFullEvent()
        {
            bool eventFullIsTriggered = false;
            var sut = new Bucket();

            sut.FullEventHandler += (o, e) => { eventFullIsTriggered = true; };
            sut.Fill(12);

            Assert.True(eventFullIsTriggered);
        }

        [Fact]
        public void PublishOverflowedEvent()
        {
            var sut = new Bucket();
            bool overflowEventIsTriggered = false;

            sut.OverflowedEventHandler += (o, e) => { overflowEventIsTriggered = true; };
            sut.Fill(99);

            Assert.True(overflowEventIsTriggered);
        }

        [Fact]
        public void PublishOverflowedAmount()
        {
            var sut = new Bucket();
            int spilledAmount = 0;
            int expectedSpilledAmount = 10;
            sut.OverflowedEventHandler += OverflowEvent;
            
            sut.Fill(22);

            Assert.Equal(expectedSpilledAmount, spilledAmount);

            void OverflowEvent(object sender, OverflowedEventArgs e)
            {
                spilledAmount = e.SpilledAmount;
            }
        }

        [Fact]
        public void AllowFillToBeCancelledWhenOverflowIsImminent()
        {
            int expectedContent = 0;
            var sut = new Bucket(0, 10);
            sut.OverflowingEventHandler += OverflowingEvent;

            sut.Fill(20);

            Assert.Equal(expectedContent, sut.Content);

            void OverflowingEvent(object sender, OverflowingEventArgs e)
            {
                e.Response = OverflowingEventResponse.Cancel;
            }
        }

        [Fact]
        public void AllowAddedAmountToBeAdjustedWhenOverflowIsImminent()
        {
            int expectedContent = 6;
            var sut = new Bucket(0, 10);
            sut.OverflowingEventHandler += OverflowingEvent;

            sut.Fill(20);

            Assert.Equal(expectedContent, sut.Content);

            void OverflowingEvent(object sender, OverflowingEventArgs e)
            {
                e.Response = OverflowingEventResponse.FillPartially;
                e.AmountToBeAdded = 6;
            }
        }

        [Fact]
        public void AllowToBeFilledToTheBrimWhenOverflowIsImminent()
        {
            int expectedContent = 10;
            var sut = new Bucket(0, 10);
            sut.OverflowingEventHandler += OverflowingEvent;

            sut.Fill(20);

            Assert.Equal(expectedContent, sut.Content);

            void OverflowingEvent(object sender, OverflowingEventArgs e)
            {
                e.Response = OverflowingEventResponse.FillToBrim;
            }
        }
        #endregion

        #region Exceptions
        [Fact]
        public void ThrowNegativeAmountExceptionWhenFilledWithNegativeAmount()
        {
            var sut = new Bucket();

            Assert.Throws<NegativeAmountException>(() => sut.Fill(-1));
        }

        [Fact]
        public void ThrowNegativeAmountExceptionWhenEmptiedWithNegativeAmount()
        {
            var sut = new Bucket();

            Assert.Throws<NegativeAmountException>(() => sut.Empty(-1));
        }

        [Fact]
        public void ThrowNegativeAmountExceptionWhenCreatedWithNegativeContent()
        {
            Bucket sut;

            Assert.Throws<NegativeAmountException>(() => sut = new Bucket(-1));
        }

        [Fact]
        public void ThrowSameBucketExceptionWhenFilledWithItself()
        {
            var sut = new Bucket(6);

            Assert.Throws<SameBucketException>(() => sut.Fill(sut));
        }
        #endregion

        #region Filling a Bucket with a Bucket
        [Fact]
        public void BeFillableWithOtherBucket()
        {
            var sut = new Bucket();
            int expectedContent = 6;
            var otherBucket = new Bucket(expectedContent);

            sut.Fill(otherBucket);

            Assert.Equal(expectedContent, sut.Content);
        }

        [Fact]
        public void BeEmptyWhenUsedToFillOtherBucket()
        {
            var sut = new Bucket();
            var bucketToFill = new Bucket();
            int expectedContent = 0;

            bucketToFill.Fill(sut);

            Assert.Equal(expectedContent, sut.Content);
        }

        [Fact]
        public void BeEmptiedPartiallyWhenUsedToFillOtherBucketToBrim()
        {
            int expectedContent = 10;
            var sut = new Bucket(20, 20);
            var bucketToFill = new Bucket(0, 10);
            bucketToFill.OverflowingEventHandler += OverflowingEvent;

            bucketToFill.Fill(sut);

            Assert.Equal(expectedContent, sut.Content);

            void OverflowingEvent(object sender, OverflowingEventArgs e)
            {
                e.Response = OverflowingEventResponse.FillToBrim;
            }
        }

        [Fact]
        public void BeEmptiedPartiallyWhenUsedToFillOtherBucketPartially()
        {
            int expectedContent = 14;
            var sut = new Bucket(20, 20);
            var bucketToFill = new Bucket(0, 10);
            bucketToFill.OverflowingEventHandler += OverflowingEvent;

            bucketToFill.Fill(sut);

            Assert.Equal(expectedContent, sut.Content);

            void OverflowingEvent(object sender, OverflowingEventArgs e)
            {
                e.Response = OverflowingEventResponse.FillPartially;
                e.AmountToBeAdded = 6;
            }
        }

        [Fact]
        public void NotBeEmptiedWhenUsedToFillOtherBucketAndOverflowIsCancelled()
        {
            int expectedContent = 20;
            var sut = new Bucket(expectedContent, expectedContent);
            var bucketToFill = new Bucket();
            bucketToFill.OverflowingEventHandler += OverflowingEvent;

            bucketToFill.Fill(sut);

            Assert.Equal(expectedContent, sut.Content);

            void OverflowingEvent(object sender, OverflowingEventArgs e)
            {
                e.Response = OverflowingEventResponse.Cancel;
            }
        }
        #endregion
    }
}
