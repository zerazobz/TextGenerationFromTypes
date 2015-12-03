using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextGeneratorFromTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Generate Text From Types Searched in DLL";
            if (args.Length == 0)
            {
                return;
            }
            
            string dllName = args.GetValue(0).ToString();
            string typeNameToSearched = args.GetValue(1).ToString();
            string dllPathFolder = String.Empty;
//#if DEBUG
//            dllPathFolder = @"C:\Users\Erick\Documents\Neo\Tools\AdoTemplateGenerator\AdoTemplateGenerator\AdoTemplateGenerator\bin\Debug\";
//#else
            dllPathFolder = Environment.CurrentDirectory;
//#endif

            string dllPathName = String.Empty;
            Console.WriteLine($"Nombre de la DLL: {dllName}");
            if (Directory.Exists(dllPathFolder))
            {
                dllPathName = Path.GetFullPath(Path.Combine(dllPathFolder, dllName));
            }
            else
                return;


            AssemblyName assemblyData = AssemblyName.GetAssemblyName(dllPathName);

            AppDomain currentAppDomain = AppDomain.CurrentDomain;
            currentAppDomain.AssemblyResolve += (sen, eve) =>
            {
                return Assembly.Load(assemblyData);
            };

            var assemblyLoaded = Assembly.Load(assemblyData);
            var typeSearched = assemblyLoaded.GetTypes().Where(t => t.Name.IndexOf(typeNameToSearched, StringComparison.OrdinalIgnoreCase) != -1).FirstOrDefault();
            var instanceOfType = Activator.CreateInstance(typeSearched);

            var typeProperties = typeSearched.GetProperties();
            Console.WriteLine(Environment.NewLine);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Explorando Propiedades::");
            foreach (var iProperty in typeProperties)
            {
                Console.WriteLine("\tSe encontro la propiedad: {0,15}\tcon el tipo: {1,-15}", iProperty.Name, iProperty.PropertyType.FullName);
            }

            Console.ReadKey();
        }
    }
}
