using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MedianMaintenance_with_Heaps.Models;

namespace MedianMaintenance_with_Heaps
{
    /// <summary>
    /// This program maintains easy access of the median value of a set of numbers as they are added one by one
    /// to the set via the median maintainence algorithm.
    /// </summary>
    public class Program
    {

        static void Main(string[] args)
        {
            // Get all lines from the text file provided
            String filePath = @"..\..\TestFiles\Median.txt";
            string[] lines = File.ReadAllLines(filePath);
            List<int> medians = new List<int>();

            MedianMaintenanceAlgorith algorith = new MedianMaintenanceAlgorith();

            // Add elements to the two heaps according to the median maintenance algorithm (O(log n))
            foreach (var line in lines)
            {
                int number = Convert.ToInt32(line);
                int median = algorith.GetMedianWithNewItem(number);
                // Maintain a running list of median values as numbers are added one by one from the file
                medians.Add(median);
            }
            // Write the last four digits of the sum of all median values to the console

            long sum = 0;
            foreach (var median in medians)
            {
                sum += median;
            }

            Console.WriteLine(sum % 10000 + " " + medians.Sum() % 10000);
            Console.ReadKey();
        }
    }
}
