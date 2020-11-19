using Buckets.CustomEventArgs;
using System;
using System.Dynamic;

namespace Buckets
{
    class Program
    {
        static void Main(string[] args)
        {
            var sut = new Bucket(20, 20);
            var bucketToFill = new Bucket(0, 10);
            bucketToFill.OverflowingEventHandler += OverflowingEvent;

            DisplayContainerStats(sut);

            bucketToFill.Fill(sut);

            DisplayContainerStats(sut);


            void OverflowingEvent(object sender, OverflowingEventArgs e)
            {
                e.Response = OverflowingEventResponse.FillPartially;
                e.AmountToBeAdded = 6;
            }

            Console.ReadKey();
        }

        static void DisplayContainerStats(Container container)
        {
            Console.WriteLine($"Container type: {container.GetType().Name}");
            Console.WriteLine($"Maximum capacity: {container.Capacity}, Current contents: {container.Content}");
        }

        static void ContainerFull(object sender, FullEventArgs e)
        {
            Console.WriteLine($"Full event published.");
        }

        static void ContainerOverflowed(object sender, OverflowedEventArgs e)
        {
            Console.WriteLine($"Overflowed event published. Amount spilled: {e.SpilledAmount}.");
        }

        static void ContainerOverflowing(object sender, OverflowingEventArgs e)
        {
            var cont = sender as Container;

            if (cont != null)
            {
                Console.WriteLine($"Overflowing event published.");
            }
        }
    }
}
