using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Manifest
{
    class Program
    {
        const string projectName = "EnumRun";
        const string debugDir = @"..\..\..\EnumRun\bin\Debug";
        const string releaseDir = @"..\..\..\EnumRun\bin\Release";
        
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            PSD1.Create(projectName, debugDir);
            PSD1.Create(projectName, releaseDir);
            PSM1.Create(projectName, debugDir);
            PSM1.Create(projectName, releaseDir);
        }
    }
}
