using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort___Count_Inversions
{
    class Program
    {
        
        static void Main(string[] args)
        {
            long[] intsFromFile = ReadFrom(@"c:\users\vlad\documents\visual studio 2013\Projects\MergeSort - Count Inversions\MergeSort - Count Inversions\IntegerArray.txt");

            //long[] intsFromFile = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            
            long[] mergedArray;

            Console.WriteLine(SortAndCount(intsFromFile, out mergedArray).ToString());
            Console.ReadLine();
        }

        static long[] ReadFrom(string file)
        {
            string line;
            long[] intsFromFile = new long[100000];
            long i = 0; ;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    long newRecord = Convert.ToInt64(line); /* parse line */
                    intsFromFile[i++] = newRecord;
                }
            }
            Console.WriteLine(i.ToString());
            return intsFromFile;
        }

        public static long SortAndCount(long[] array, out long[] merged)
        {
            if (array.Length == 1)
            {
                merged = array;
                return 0;
            }
            else
            {
                long[] firstHalf = new long[array.LongLength / 2];
                long[] secondHalf = new long[array.LongLength - array.LongLength / 2];

                long[] B = new long[array.LongLength];
                long[] C = new long[array.LongLength];
                long[] D = new long[array.LongLength];

                Array.Copy(array, 0, firstHalf, 0, array.LongLength / 2);
                Array.Copy(array, array.LongLength / 2, secondHalf, 0, array.LongLength - array.LongLength / 2);

                long x = SortAndCount(firstHalf, out B);

                long y = SortAndCount(secondHalf, out C);

                long z = MergeAndCountSplitInv(B, C, out D);

                merged = D;
                return x + y + z;
            }

        }

        private static long MergeAndCountSplitInv(long[] firstHalf, long[] secondHalf, out long[] D)
        {
            long n = firstHalf.LongLength + secondHalf.LongLength;
            long i = 0;
            long j = 0;
            long[] merged = new long[n];
            long inversions = 0;

            for (long k = 0; k < n; k++)
            {
                if (firstHalf.LongLength <= i)
                {
                    Array.Copy(secondHalf, j, merged, i + j, secondHalf.LongLength - j);
                    break;
                }

                if (secondHalf.LongLength <= j)
                {
                    Array.Copy(firstHalf, i, merged, i + j, firstHalf.LongLength - i);
                    break;
                }

                if (firstHalf[i] < secondHalf[j])
                {
                    merged[k] = firstHalf[i];
                    i++;
                }
                else
                {
                    merged[k] = secondHalf[j];
                    j++;
                    inversions += firstHalf.LongLength - i;
                }
            }

            D = merged;
            return inversions;
        }




    }
}
