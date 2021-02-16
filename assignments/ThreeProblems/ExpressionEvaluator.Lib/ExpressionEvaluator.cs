using ExpressionEvaluator.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionEvaluator.Lib
{
    public class ExpressionEvaluator
    {
        private Dictionary<string, Operator> operators = new Dictionary<string, Operator>
        {
            { "^", new Operator { Name="^", Precedence=4, RightAssociative=true } },
            { "*", new Operator { Name="*", Precedence=3, RightAssociative=false} },
            { "/", new Operator { Name="/", Precedence=3, RightAssociative=false} },
            { "+", new Operator { Name="+", Precedence=2, RightAssociative=false} },
            { "-", new Operator { Name="-", Precedence=2, RightAssociative=false} }
        };

        public decimal EvaluateExpression(string expression)
        {
            List<string> tokens = ParseExpression(expression);
            List<string> rpn = ToRpn(tokens);
            decimal output = CalculateRpn(rpn);
            return output;
        }

        public List<string> ParseExpression(string expression)
        {
            List<string> tokens = new List<string>();

            foreach (var match in Regex.Matches(expression, @"([*+/\-)(])|([0-9]+)"))
            {
                tokens.Add(match.ToString());
            }

            return tokens;
        }

        public List<string> ToRpn(List<string> tokens)
        {
            var operatorStack = new Stack<string>();
            var output = new List<string>();

            foreach(string token in tokens)
            {
                if (IsANumber(token))
                {
                    output.Add(token.ToString());
                }
                else if (operators.TryGetValue(token, out var op1))
                {
                    while (operatorStack.Count > 0 && operators.TryGetValue(operatorStack.Peek(), out var op2))
                    {
                        int c = op1.Precedence.CompareTo(op2.Precedence);
                        if (c < 0 || !op1.RightAssociative && c <= 0)
                        {
                            output.Add(operatorStack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    operatorStack.Push(token);
                }
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    string top = "";
                    while (operatorStack.Count > 0 && (top = operatorStack.Pop()) != "(")
                    {
                        output.Add(top);
                    }
                    if (top != "(") throw new ArgumentException("No matching left parenthesis.");
                }
            }
            while (operatorStack.Count > 0)
            {
                var top = operatorStack.Pop();
                if (!operators.ContainsKey(top)) throw new ArgumentException("No matching right parenthesis.");
                output.Add(top);
            }

            return output;
        }

        private bool IsANumber(string token)
        {
            return decimal.TryParse(token.ToString(), out var _);
        }

        public decimal CalculateRpn(List<string> rpnTokens)
        {
            Stack<decimal> stack = new Stack<decimal>();
            decimal number = decimal.Zero;

            foreach (string token in rpnTokens)
            {
                if (decimal.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                else
                {
                    switch (token)
                    {
                        case "^":
                            {
                                number = stack.Pop();
                                stack.Push((decimal)Math.Pow((double)stack.Pop(), (double)number));
                                break;
                            }
                        case "*":
                            {
                                stack.Push(stack.Pop() * stack.Pop());
                                break;
                            }
                        case "/":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() / number);
                                break;
                            }
                        case "+":
                            {
                                stack.Push(stack.Pop() + stack.Pop());
                                break;
                            }
                        case "-":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() - number);
                                break;
                            }
                        default:
                            throw new ArgumentException("Invalid Argument");
                    }
                }
            }
            return stack.Pop();
        }
    }
}
