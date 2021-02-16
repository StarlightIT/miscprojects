using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anagram.Lib
{
    public class AnagramService
    {
        public bool CheckAnagram(string input1, string input2)
        {
            string input1Normalized = string.Concat(input1.ToLowerInvariant().OrderBy(c => c));
            string input2Normalized = string.Concat(input2.ToLowerInvariant().OrderBy(c => c));

            return input1Normalized.Equals(input2Normalized);
        }
    }
}
