using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI.DataVisualization.Charting;
using Localization = SchtormRun.Resources.Dictionaries.Localization.Resources;

namespace SchtormRun
{
    public class MathFunctionCollection : MarshalByRefObject
    {
        public readonly Dictionary<string, string> ReplacementDictionary = new Dictionary<string, string>()
        {
            { "pow", "Math.Pow" },
            { "exp", "Math.Pow" },
            { "ten", nameof(MultipliedTen) },
            { "gamma", nameof(Gamma) },
            { "gam", nameof(Gamma) },
            { "factorial", nameof(Factorial) },
            { "fact", nameof(Factorial) },
            { "fac", nameof(Factorial) },
            { "irandom", nameof(IntRandom) },
            { "random", nameof(RealRandom) },
            { "irand", nameof(IntRandom) },
            { "rand", nameof(RealRandom) },
            { "x2", nameof(Square) },
            { "hrt", nameof(HigherRoots) },
            { "sqrn", nameof(HigherRoots) },
            { "nroot", nameof(HigherRoots) },
            { "sqrt", "Math.Sqrt" },
            { "root", "Math.Sqrt" },
            { "sqr", nameof(Square) },
            { "summ", nameof(NumberSum) },
            { "sum", nameof(NumberSum) },
            { "mult", nameof(NumberMultiplication) },
            { "log10", "Math.Log10" },
            { "lg", "Math.Log10" },
            { "ln", "Math.Log" },
            { "log", "Math.Log" },
            { "asin", "Math.Asin" },
            { "arcsin", "Math.Asin" },
            { "sin", "Math.Sin" },
            { "acos", "Math.Acos" },
            { "arccos", "Math.Acos" },
            { "cos", "Math.Cos" },
            { "atan", "Math.Atan" },
            { "arctan", "Math.Atan" },
            { "tan", "Math.Tan" },
            { "atg", "Math.Atan" },
            { "arctg", "Math.Atan" },
            { "cot", nameof(Cotangent) },
            { "ctg", nameof(Cotangent) },
            { "tg", "Math.Tan" },
            { "hex", nameof(Hexadecimal) },
            { "angle", nameof(AngleToRadians) },

            { "pi", "Math.PI" },
            { "PI", "Math.PI" },
            { "eu", "Math.E" },
            { "E", "Math.E" },
            { "gratio", "const1.618033988749894848204586834365d" },
            { "GR", "const1.618033988749894848204586834365d" },

            { "degrees", "* 0.0174532925199432954743716805978692d" },
            { "deg", "* 0.0174532925199432954743716805978692d" },
            { "minutes", "* 0.0002908882086657215803975062851094d" },
            { "min", "* 0.0002908882086657215803975062851094d" },
            { "seconds", "* 0.0000048481368110953598426983608693d" },
            { "sec", "* 0.0000048481368110953598426983608693d"},

        };

        public double OutValue { get; set; }

        private static readonly Random _random = new Random();

        public double Square(double x) => Math.Pow(x, 2d);

        public double MultipliedTen(double x) => Math.Pow(10d, x);

        public double Gamma(double x) => new Chart().DataManipulator.Statistics.GammaFunction(x);

        public double Factorial(int x) => Gamma(x + 1);

        public double IntRandom(int begin, int end) => _random.Next(begin, end + 1);

        public double RealRandom(double begin, double end) => begin + _random.NextDouble() * (end - begin);

        public double NumberSum(params double[] numbers) => numbers.Sum();

        public double Cotangent(double x) => Math.Pow(Math.Tan(x), -1d);

        public double AngleToRadians(int degrees, int minutes = 0, int seconds = 0) =>
            degrees * 0.0174532925199432954743716805978692d +
            minutes * 0.0002908882086657215803975062851094d +
            seconds * 0.0000048481368110953598426983608693d;

        public double NumberMultiplication(params double[] numbers)
        {
            double multiplication = 1d;

            foreach (double number in numbers)
                multiplication *= number;

            return multiplication;
        }

        public double HigherRoots(double x, int rootGrade)
        {
            if (rootGrade < 1)
                throw new ArgumentOutOfRangeException(
                    $"{Localization.RootGradeLessOne}: " +
                    $"nroot({x}, {rootGrade})");

            if (rootGrade % 2 == 0 && x < 0)
                throw new ArgumentOutOfRangeException(
                    $"{Localization.FunctionValueNotDefined}: {rootGrade}" +
                    $"nroot({x}, {rootGrade})");

            return Math.Pow(x, 1d / rootGrade);
        }

        public int Hexadecimal(string number)
        {
            int decimalValue = 0;
            foreach (var digit in number)
                decimalValue = decimalValue * 16 + 
                    digit < '0' || digit > '9' ?
                    char.ToUpper(digit) - 'A' + 10 : digit - '0';
            
            return decimalValue;
        }
    }
}
