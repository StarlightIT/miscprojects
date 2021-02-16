using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ExpressionEvaluator.Lib;
using Xunit;

namespace ExpressionEvaluator.UnitTests
{
    public class ExpressionEvaluatorTests
    {
        private Lib.ExpressionEvaluator Evaluator = new Lib.ExpressionEvaluator();

        [Fact]
        public void PrecedenceTests()
        {
            decimal output;

            output = Evaluator.EvaluateExpression("2+3*40");
            Assert.Equal(122, output);

            output = Evaluator.EvaluateExpression("2 * 3 + 4");
            Assert.Equal(10, output);

            output = Evaluator.EvaluateExpression("2 / 3 + 4 - 1");
            Assert.Contains("3.666", output.ToString(CultureInfo.InvariantCulture));

            output = Evaluator.EvaluateExpression("2 - 3 * 4");
            Assert.Equal(-10, output);
        }
    }
}
