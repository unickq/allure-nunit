using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Allure.Commons;

namespace NUnit.Allure.Steps
{
    public static class ParameterHelper
    {
        private const string Null = "null",
            Unknown = "Unknown";

        public static List<Parameter> CreateParameters(IEnumerable<object> arguments)
            => arguments.Select(CreateParameters).ToList();

        public static Parameter CreateParameters(object argument)
        {
            if (argument == null)
            {
                return CreateParameters(Unknown, Null);
            }

            string value;
            if (argument is ICollection collection)
            {
                var sb = new StringBuilder();
                foreach (var item in collection)
                {
                    sb.Append(item + ", ");
                }
                value = sb.ToString();
            }
            else
            {
                value = argument.ToString();
            }

            return CreateParameters(argument.GetType().Name, value ?? Null);
        }

        private static Parameter CreateParameters(string name, string value)
            => new Parameter
            {
                name = name,
                value = value
            };
    }
}