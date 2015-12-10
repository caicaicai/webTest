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

            parameter = Regex.Replace(parameter, @"({{)(\w+):(\w+)(}})", (m) => {

                //MethodInfo theMethod = thisType.GetMethod(m.Groups[2].Value, BindingFlags.Instance | BindingFlags.Public);
                /*
                Console.WriteLine("0:" + m.Groups[0].Value);
                Console.WriteLine("1:" + m.Groups[1].Value);
                Console.WriteLine("2:" + m.Groups[2].Value);
                Console.WriteLine("3:" + m.Groups[3].Value);

                {{RandomNumberWithLength:16}}
                {{RandomStringWithLength:16}}
                {{RandomNumberAndStringWithLength:16}}
                {{Range:10000}}0 - 10000
                {{FullRange:10000}}00000 - 10000

                */
                switch (m.Groups[2].Value)
                {
                    case "RandomNumberWithLength":
                        return RandomNumberWithLength(m.Groups[3].Value);
                    case "RandomStringWithLength":
                        return RandomStringWithLength(m.Groups[3].Value);
                    case "RandomNumberAndStringWithLength":
                        return RandomNumberAndStringWithLength(m.Groups[3].Value);
                    default:
                        return parameter;
                }

            });

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

        private static string RandomStringWithLength(string param)
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

        private static string RandomNumberAndStringWithLength(string param)
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


    }
}
