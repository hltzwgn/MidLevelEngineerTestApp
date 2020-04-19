using System.Collections.Generic;
using System;
namespace Ipro.Platform.CodingTest.App
{
    class Program
    {
        public static ResourceSearchManager _searchEngine = new ResourceSearchManager();
        static void Main(string[] args)
        {
            string searchTerm = string.Empty;
            Console.WriteLine("Insert Search Term or type \"QUIT!\" to exit");
            do
            {
                searchTerm = GatherInput();
            }
            while (Search(searchTerm));
            Console.WriteLine("Quitting!");
        }
        static bool Search(string searchTerm)
        {
            if (!searchTerm.Equals("QUIT!"))
            {
                OutPutResults(PerformSearch(searchTerm));
                return true;
            }
            else
                return false;
        }
        static string GatherInput()
        {
            Console.Write("Search Term: ");
            return Console.ReadLine().Trim();
        }
        static List<TxtResult> PerformSearch(string searchTerm)
        {
            
            List<TxtResult> result = _searchEngine.SearchResource(searchTerm);
            result.Sort((x, y) => x.Occurrences.CompareTo(y.Occurrences));
            return result;
        }
        static void OutPutResults(List<TxtResult> result)
        {
            for(int i = result.Count -1; i >= 0; --i)
            {
                if (result[i].Occurrences > 0)
                Console.WriteLine($"Filename: {result[i].GetShortName()}, Occurences: {result[i].Occurrences}");
            }
        }
    }
}
