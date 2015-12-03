using System;
using System.Collections.Generic;
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
            //if(args.Length == 0)
            //{
            //    return;
            //}

            //string dllPathName = args.GetValue(0).ToString();
            string dllPathName = Console.ReadLine();
            Console.WriteLine(dllPathName);
            AssemblyName assemblyData = AssemblyName.GetAssemblyName(dllPathName);

            AppDomain currentAppDomain = AppDomain.CurrentDomain;
            currentAppDomain.AssemblyResolve += (sen, eve) =>
            {
                return Assembly.Load(assemblyData);
            };

            var assemblyLoaded = Assembly.Load(assemblyData);
            var typeSearched = assemblyLoaded.GetTypes().Where(t => t.Name.Contains("ReaderColumnModel")).FirstOrDefault();
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
