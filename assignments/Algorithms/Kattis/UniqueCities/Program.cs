using System;
using System.Collections.Generic;

namespace UniqueCities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string line;
            List<string> input = new List<string>();

            while ((line = Console.ReadLine()) != null)
            {
                input.Add(line);
            }

            List<string> uniqueCities = GetUniqueCities(input);

            foreach(var number in uniqueCities)
            {
                Console.WriteLine(number);
            }
        }

        public static List<string> GetUniqueCities(List<string> input)
        {
            List<string> numOfUniqueCities = new List<string>();

            int numOfTestCases = int.Parse(input[0]);
            input.RemoveAt(0);
            HashSet<string> uniqueCities = null;

            foreach(var line in input)
            {
                if (int.TryParse(line, out _))
                {
                    if (uniqueCities != null) 
                    { 
                        numOfUniqueCities.Add(uniqueCities.Count.ToString()); 
                    }

                    uniqueCities = new HashSet<string>();
                }
                else
                {
                    uniqueCities.Add(line);
                }
            }
            numOfUniqueCities.Add(uniqueCities.Count.ToString());

            return numOfUniqueCities;
        }
    }
}
