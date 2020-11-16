using System;
using System.Dynamic;

namespace Buckets
{
    class Program
    {
        static void Main(string[] args)
        {
            var bucket = new Bucket();
            var bucket2 = new Bucket(19, 19);

            DisplayContainerStats(bucket);

            Console.WriteLine($"Filling {nameof(bucket)} with bucket2, 'Overflowed' event expected.");
            bucket.Fill(bucket2);

            DisplayContainerStats(bucket);

            Console.ReadKey();
        }

        static void DisplayContainerStats(Container container)
        {
            Console.WriteLine($"Container type: {container.GetType().Name}");
            Console.WriteLine($"Maximum capacity: {container.Capacity}, Current contents: {container.Content}");
        }

        static void ContainerFull(object sender, ContentEventArgs e)
        {
            Console.WriteLine($"{e.Message}");
        }

        static void ContainerOverflowed(object sender, ContentEventArgs e)
        {
            Console.WriteLine($"{e.Message} Amount spilled: {e.SpilledAmount}.");
        }

        static void ContainerOverflowing(object sender, ContentEventArgs e)
        {
            var cont = sender as Container;

            if (cont != null)
            {
                Console.WriteLine($"{e.Message} Amount that will be spilled: {e.SpilledAmount}. Please don't add more than {cont.Capacity - cont.Content}.");
            }
        }
    }
}
