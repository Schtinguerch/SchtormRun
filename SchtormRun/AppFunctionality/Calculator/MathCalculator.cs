using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tech.CodeGeneration;
using Tech.CodeGeneration.Compilers;

namespace SchtormRun
{
    public class MathCalculator : MarshalByRefObject
    {
        public MathFunctionCollection MathFunctions { get; set; }

        public double Value { get; set; }

        public MathCalculator() =>
            MathFunctions = new MathFunctionCollection();

        public double Compute(string expression)
        {
            var parts = expression.Split(':');
            var additionalArguments = parts.Length == 2 ? parts.First().Replace(" ", "") : "";

            expression = $"{nameof(MathCalculator)}.{nameof(Value)} = {ReplacedFunctionNames(parts.Last())};";

            using (var sandBox = new Sandbox())
            {
                var parameterList = new List<CodeParameter>() 
                {
                    CodeParameter.Create(nameof(MathFunctions), MathFunctions),
                    CodeParameter.Create(nameof(MathCalculator), this),
                };

                var parameterValues = Values(additionalArguments, parameterList);

                var code = CodeGenerator.CreateCode<double>(
                    sandbox: sandBox,
                    language: CS.Compiler,
                    sourceCode: expression,
                    usingNamespaces: null,
                    referencedAssemblies: null,
                    parameterInfos: parameterList.ToArray());

                code.Execute(parameterValues.ToArray());
            }

            return Value;
        }

        private string ReplacedFunctionNames(string expression)
        {
            foreach (var pair in MathFunctions.ReplacementDictionary)
            {
                if (pair.Value.Contains("Math.") || pair.Value.Contains("*"))
                    expression = expression.Replace(pair.Key, pair.Value);
                else if (pair.Value.Contains("const"))
                    expression = expression.Replace(pair.Key, pair.Value.Replace("const", ""));
                else
                    expression = expression.Replace(pair.Key, $"{nameof(MathFunctions)}.{pair.Value}");
            }

            return expression;
        }
        
        private List<object> Values(string argumentInput, List<CodeParameter> parameterList)
        {
            var objectValues = new List<object>();

            foreach (var parameter in parameterList)
                objectValues.Add(parameter.Value);

            var argumentsDefinitions = argumentInput.Split(',');

            if (argumentInput != "")
                foreach (var argumentsDefinition in argumentsDefinitions)
                {
                    var tokens = argumentsDefinition.Split('=');

                    var argumentName = tokens.First();
                    var argumentValue = double.Parse(tokens.Last(), CultureInfo.InvariantCulture);

                    parameterList.Add(new CodeParameter(argumentName, typeof(double)));
                    objectValues.Add(argumentValue);
                }

            return objectValues;
        }
    }
}
