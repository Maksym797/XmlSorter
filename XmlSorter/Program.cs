using System;
using System.IO;
using System.Xml.Linq;
using XmlSorter.Handlers;

namespace XmlSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO add args validation with print instruction service

            //args = new string[] { "./Files/Solution.xml", "RootComponents", "RootComponent", "schemaName,type" };
            //args = new string[] { "./Files/Solution.xml", "MissingDependencies", "MissingDependency", "Required/type,Dependent\\type,Required/schemaName" };

            //TODO OutFoRange
            if (args == null || args.Length != 4)
            {
                PrintInstruction();
                return;
            }

            var filesPath = GetFilesPath(args[0]);
            var rootElementName = args[1];
            var elementName = args[2];
            var sortByList = args[3].Trim(',').Split(',');

            var sortingHandler = new SortingHandler(rootElementName, elementName, sortByList);

            foreach (var filePath in filesPath)
            {
                var xDocument = XDocument.Load(filePath);

                xDocument = sortingHandler.Handle(xDocument);
                xDocument.Save(filePath);
            }
        }

        private static void PrintInstruction()
        {
            Console.WriteLine("Welcome to XMl sorter!");
            Console.WriteLine("");
            Console.WriteLine("Expected input examples:");
            Console.WriteLine("* \"./ Files/Solution.xml\", \"RootComponents\", \"RootComponent\", \"schemaName, type\"");
            Console.WriteLine("* \"./ Files/Solution.xml\", \"MissingDependencies\", \"MissingDependency\", \"Required/type, Dependent\\type, Required/schemaName\"");
            Console.WriteLine("* \"./ Files/\", \"MissingDependencies\", \"MissingDependency\", \"Required/type, Dependent\\type, Required/schemaName\" - all .xml files in folder and `SearchOption.AllDirectories`");
            Console.WriteLine("TODO Add `Documentation :)`");
            Console.WriteLine("");
            Console.ReadLine();
        }

        private static string[] GetFilesPath(string path)
        {
            if (path.EndsWith(".xml"))
            {
                return new[] { path };
            }
            return Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
        }
    }
}
