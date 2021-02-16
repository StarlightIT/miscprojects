using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionEvaluator.Lib.Models
{
    public class Operator
    {
        public string Name { get; set; }
        public int Precedence { get; set; }
        public bool RightAssociative { get; set; }
    }
}
