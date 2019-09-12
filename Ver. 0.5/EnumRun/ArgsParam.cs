using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EnumRun
{
    class ArgsParam
    {
        public static void CheckArgs(string[] args)
        {
            if(args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "-v":
                    case "--v":
                    case "--version":
                        Version ver = Assembly.GetExecutingAssembly().GetName().Version;
                        Console.WriteLine(ver.ToString());
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
