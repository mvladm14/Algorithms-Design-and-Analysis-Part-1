using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace _2_SUM_alg_HTable
{
    /// <summary>
    /// This program is an illustration of the two-sum problem described in the course, where we want to determine whether
    /// or not a target value can be represented by the sum of any two unique elements in a given list.
    /// 
    /// In order to improve the performance, a parallel library was employed so that multiple threads can run in parallel,
    /// thus making use of all the CPU cores
    /// </summary>
    public class Program
    {
        private static IList<long> _list;
        private static HashSet<long> _hashSet;

        static void Main(string[] args)
        {
            CreateHastSetFromFile(@"..\..\TestFiles\algo1_programming_prob_2sum.txt");

            const int min = -10000;
            const int max = 10000;

            // Running sum of numbers that can be represented by a sum of two unique elements in the list
            int total = 0;

            Parallel.For<int>(min, max, () => 0, (j, loop, subtotal) =>
            {
                subtotal += TwoSums(j);
                return subtotal;
            },
                x => Interlocked.Add(ref total, x)
            );

            Console.WriteLine(total);
            Console.ReadLine();
        }

        /// <summary>
        /// Determines whether there are two unique elements in the list that add up to the target value.
        /// </summary>
        /// <param name="target">The value used as the target value (sum of two unique elements in list).</param>
        /// <returns>Returns one if the target value is a sum of two unique elements in a list of numbers. Returns zero otherwise.</returns>
        private static int TwoSums(int target)
        {
            foreach (long n in _list)
            {
                long complement = target - n;
                // Spec states that only unique values can be added to one another
                // If the complement is the number itself, there is not a unique element in the list.
                if (complement == n)
                    return 0;
                if (_hashSet.Contains(complement))
                    return 1;
            }
            return 0;
        }

        private static void CreateHastSetFromFile(string path)
        {
            Console.WriteLine("Reading file...");
            _list = new List<long>();
            _hashSet = new HashSet<long>();
            using (var reader = File.OpenText(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    long value = Convert.ToInt64(line);
                    _hashSet.Add(value);
                    _list.Add(value);
                }
            }
            Console.WriteLine("Finished reading.");
        }
    }
}
