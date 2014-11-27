using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WordsAPI.WordAPI wordAPI = new WordsAPI.WordAPI();
            string[,] testMatrix = { { "R", "H", "P", "AN" }, { "M", "A", "E", "Y" }, { "E", "R", "G", "S" }, { "R", "R", "E", "D" } };
            wordAPI.MatrixInit();
            do
            {
                Console.WriteLine("16 upper case strings, each in a line");
                for (int i = 0; i < 4; i++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        testMatrix[i, y] = Console.ReadLine();
                    }
                }
                Console.WriteLine("3 High Value Words:");
                string[] highValue = new string[] { "AN", "Y", "R" };

                for (int qq = 0; qq < 3; qq++)
                {
                    highValue[qq] = Console.ReadLine();
                }
                Stopwatch ssss = new Stopwatch();
                ssss.Start();
                List<string> Results = wordAPI.ProcessMatrix(testMatrix);
                ssss.Stop();
                var DistinctResults = Results.Distinct().OrderByDescending(aaa => aaa.Length);
                var list1 = DistinctResults.Where(www => ((www.Length - www.Replace(highValue[0], String.Empty).Length) / highValue[0].Length) > 0).OrderByDescending(aaaa => aaaa.Length).ToList<string>();
                var list2 = DistinctResults.Where(www => ((www.Length - www.Replace(highValue[1], String.Empty).Length) / highValue[1].Length) > 0).OrderByDescending(aaaa => aaaa.Length).Except(list1).ToList<string>();
                var list3 = DistinctResults.Where(www => ((www.Length - www.Replace(highValue[2], String.Empty).Length) / highValue[2].Length) > 0).OrderByDescending(aaaa => aaaa.Length).Except(list1).Except(list2).ToList<string>();
                var otherResults = DistinctResults.Except(list1).Except(list2).Except(list3);
                var maxCount = new List<Int32> { list1.Count, list2.Count, list3.Count };
                int rs = maxCount.Max();
                for (int rr = 0; rr < rs; rr++)
                {
                    Console.WriteLine("{0} {1} {2} {3}", list1.ElementAtOrDefault(rr), list2.ElementAtOrDefault(rr), list3.ElementAtOrDefault(rr), otherResults.ElementAtOrDefault(rr));
                }
                Console.WriteLine(ssss.Elapsed + "----" + DistinctResults.Count() + ";; Any key - repeat, else - escape");
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            // Console.ReadKey();
        }
    }
}
