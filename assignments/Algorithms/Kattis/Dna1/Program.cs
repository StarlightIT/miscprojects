using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dna1
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

            List<string> output = CompareStrings(input);
            foreach(string outputLine in output)
            {
                Console.WriteLine(outputLine);
            }
        }

        public static List<string> CompareStrings(List<string> input)
        {
            List<string> output = new List<string>();

            int dataSets = int.Parse(input[0]);
            input.RemoveAt(0);
            List <List<string>> inputSets = input.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 2).Select(x => x.Select(v => v.Value).ToList()).ToList();

            foreach(List<string> inputSet in inputSets)
            {
                output.Add(inputSet[0]);
                output.Add(inputSet[1]);
                string differences = GetDifferences(inputSet[0], inputSet[1]);
                output.Add(differences);
                output.Add("");
            }

            return output;
        }

        public static string GetDifferences(string input1, string input2)
        {
            StringBuilder differences = new StringBuilder(input1.Length);

            byte[] input1Bytes = Encoding.ASCII.GetBytes(input1);
            byte[] input2Bytes = Encoding.ASCII.GetBytes(input2);

            for (int i=0; i<input1Bytes.Length; i++)
            {
                if (input1Bytes[i] - input2Bytes[i] != 0)
                {
                    differences.Append('*');
                }
                else
                {
                    differences.Append('.');
                }
            }

            return differences.ToString();
        }
    }
}
