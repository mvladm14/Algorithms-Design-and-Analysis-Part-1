using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort___Count_Comparisons
{
    class Program
    {
        private static long totalComparisons;
        private static int pivoteMode = 0;
        static void Main(string[] args)
        {
            List<int> intsFromFile = ReadFrom(@"c:\users\vlad\documents\visual studio 2013\Projects\QuickSort - Count Comparisons\QuickSort - Count Comparisons\QuickSort.txt");

            List<int> quickSortedArray;

            totalComparisons = 0;

            Console.WriteLine("\nEnter pivode mode: 0, 1 (for last element), 2 (for middle element)");
            string line = Console.ReadLine();
            pivoteMode = Convert.ToInt32(line);

            QuickSortAndCountComparisons(intsFromFile, out quickSortedArray);

            /*
            foreach (int el in quickSortedArray)
            {
                Console.Write(el + " ");
            }
             */

            Console.WriteLine("\nTotal Comparisons = " + totalComparisons.ToString());

            Console.ReadLine();
        }

        private static void QuickSortAndCountComparisons(List<int> array, out List<int> quickSortedArray)
        {
            if (array == null || array.Count <= 1)
            {
                quickSortedArray = array;
            }
            else
            {
                int middle = array.Count % 2 == 1 ? array.Count / 2 : array.Count / 2 - 1;
                
                int p = ChoosePivot(array, middle);
                totalComparisons += array.Count - 1;

                List<int> firstPart = new List<int>();
                List<int> secondPart = new List<int>();

                PartitionArrayAroundPivot(array, p, firstPart, secondPart);

                List<int> sortedFirstPart = new List<int>();
                List<int> sortedSecondPart = new List<int>();

                QuickSortAndCountComparisons(firstPart, out sortedFirstPart);
                QuickSortAndCountComparisons(secondPart, out sortedSecondPart);

                sortedFirstPart.Add(p);
                quickSortedArray = sortedFirstPart.Concat(sortedSecondPart).ToList<int>();

            }

        }

        private static void PartitionArrayAroundPivot(List<int> array, int p, List<int> firstPart, List<int> secondPart)
        {
            int l = array.IndexOf(p);
            int i = l + 1;

            for (int j = l + 1; j < array.Count; j++)
            {
                if (array[j] < p)
                {
                    int aux = array[j];
                    array[j] = array[i];
                    array[i] = aux;
                    i++;
                }
            }
            
            int aux2 = array[l];
            array[l] = array[i - 1];
            array[i - 1] = aux2;
            
            for (int k = 0; k < array.Count; k++)
            {
                if (array[k] < p)
                {
                    firstPart.Add(array[k]);
                }
                else if (array[k] > p)
                {
                    secondPart.Add(array[k]);
                }
            }
        }


        private static int ChoosePivot(IList<int> array, int position)
        {
            int pivot = -1;
            int last = array.Count - 1; 
            int middle = array.Count % 2 == 1 ? array.Count / 2 : array.Count / 2 - 1;

            if (position == 0)
            {
                pivot = array.First();
            }
            else if (position == last)
            {
                int aux = array.First();
                array[0] = array.Last();
                array[array.Count - 1] = aux;
                pivot = array.First();
            }
            else if (position == middle)
            {
                List<int> possiblePivots = new List<int>();
                possiblePivots.Add(array[0]);
                possiblePivots.Add(array[last]);
                possiblePivots.Add(array[middle]);
                possiblePivots.Sort();

                int median = array.IndexOf(possiblePivots[1]);

                int aux = array[0];
                array[0] = array[median];
                array[median] = aux;

                pivot = array.First();
            }
            //Console.WriteLine(pivot.ToString());
            return pivot;
        }


        static List<int> ReadFrom(string file)
        {
            string line;
            List<int> intsFromFile = new List<int>();
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    int newRecord = Convert.ToInt32(line); /* parse line */
                    intsFromFile.Add(newRecord);
                }
            }
            return intsFromFile;
        }
    }
}
