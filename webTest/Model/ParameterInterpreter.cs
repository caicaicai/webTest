using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;

namespace webTest.Model
{
    public class ParameterInterpreter
    {
        public ParameterInterpreter()
        {

        }
        public string generater(string parameter)
        {
            Console.WriteLine("handel with " + parameter);

            parameter = Regex.Replace(parameter, @"({{)(\w+):(\w+)(}})", (m) => {

                //MethodInfo theMethod = thisType.GetMethod(m.Groups[2].Value, BindingFlags.Instance | BindingFlags.Public);
                return InvokeStringMethod("ParameterInterpreter", "RandomNumberWithLength", "16");

            });
            Console.WriteLine("handel result : " + parameter);
            return parameter;
        }

        private static string RandomNumberWithLength(string param)
        {
            int CodeLength = Int32.Parse(param);
            Random random = new Random();
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < CodeLength; i++)
            {
                output.Append(random.Next(0, 9));
            }

            return output.ToString();
        }

        public static string InvokeStringMethod(string typeName, string methodName, string stringParam)
        {
            // Get the Type for the class
            Type calledType = Type.GetType(typeName);

            // Invoke the method itself. The string returned by the method winds up in s.
            // Note that stringParam is passed via the last parameter of InvokeMember,
            // as an array of Objects.
            String s = (String)calledType.InvokeMember(
                            methodName,
                            BindingFlags.InvokeMethod | BindingFlags.Public |
                                BindingFlags.Static,
                            null,
                            null,
                            new Object[] { stringParam });

            // Return the string that was returned by the called method.
            return s;
        }
    }
}
