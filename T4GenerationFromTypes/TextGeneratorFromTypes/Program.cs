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

            Console.ReadKey();
        }
    }
}
