using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WordsAPI
{
    public class WordAPI
    {
        //Variables
        string[,] Matrix;
       // Microsoft.Office.Interop.Word.Application WordCheckApp;
        int i, j;
        string one, two;
        List<string>[] DictionariesAtoZ;
        List<string> WordsFound_List;
        // object myLock;
        //Methods
        public void MatrixInit()
        {
            //WordCheckApp = new Microsoft.Office.Interop.Word.Application();
            DictionariesAtoZ = new List<string>[26];
            //myLock = new object();
            for (int i = 0; i < 26; i++)
            {
                DictionariesAtoZ[i] = File.ReadAllLines(System.IO.Directory.GetCurrentDirectory() + "\\Dictionary\\" + ((char)(65 + i)).ToString() + ".txt").Where(aa => aa.Length > 2).ToList<string>();
            }
            //     
        }
        public List<string> ProcessMatrix(string[,] myMatrix)
        {
            List<Thread> workerThreads = new List<Thread>();
            // Stopwatch ssss = new Stopwatch();
            //  ssss.Start();
            Matrix = myMatrix;
            WordsFound_List = new List<string>();
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    // ssss.Start();
                    Thread thread = new Thread(() => FindWords_Recursive(i, j, Matrix[i, j], i.ToString() + j + ":"));
                    workerThreads.Add(thread);
                    thread.Start();
                    //   ssss.Stop();
                    //    Console.WriteLine(i + ":" + j);
                }
            }
            foreach (Thread thread in workerThreads)
            {
                thread.Join();
            }
            return WordsFound_List;
        }

        private void FindWords_Recursive(int i, int j, string formingWord, string visitedPath)
        {
            // Console.WriteLine(visitedPath);
            if (formingWord.Contains("\\"))
            {
                int p = formingWord.IndexOf("\\");
                one = formingWord.Substring(0, formingWord.Length - 2);
                two = formingWord.Substring(0, formingWord.Length - 3) + formingWord.ElementAt(p + 1);
            }
            else { one = formingWord; two = string.Empty; }
            if (!string.IsNullOrEmpty(formingWord) && formingWord.Length > 2 && DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].BinarySearch(formingWord) >= 0)
            {
                lock (WordsFound_List)
                {
                    WordsFound_List.Add(formingWord);
                }
            }
            if (i + 1 < 4 && j + 0 < 4 && !visitedPath.Contains((i + 1).ToString() + (j + 0).ToString() + ":") && !string.IsNullOrEmpty(DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].Where(www => ((www.Length - www.Replace(formingWord, String.Empty).Length) / formingWord.Length) > 0).FirstOrDefault()))
                FindWords_Recursive(i + 1, j + 0, formingWord + Matrix[i + 1, j + 0], visitedPath + (i + 1).ToString() + (j + 0).ToString() + ":");
            if (i + 0 < 4 && j + 1 < 4 && !visitedPath.Contains((i + 0).ToString() + (j + 1).ToString() + ":") && !string.IsNullOrEmpty(DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].Where(www => ((www.Length - www.Replace(formingWord, String.Empty).Length) / formingWord.Length) > 0).FirstOrDefault()))
                FindWords_Recursive(i + 0, j + 1, formingWord + Matrix[i + 0, j + 1], visitedPath + (i + 0).ToString() + (j + 1).ToString() + ":");
            if (i + 1 < 4 && j + 1 < 4 && !visitedPath.Contains((i + 1).ToString() + (j + 1).ToString() + ":") && !string.IsNullOrEmpty(DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].Where(www => ((www.Length - www.Replace(formingWord, String.Empty).Length) / formingWord.Length) > 0).FirstOrDefault()))
                FindWords_Recursive(i + 1, j + 1, formingWord + Matrix[i + 1, j + 1], visitedPath + (i + 1).ToString() + (j + 1).ToString() + ":");
            if (i - 1 > -1 && j + 1 < 4 && !visitedPath.Contains((i - 1).ToString() + (j + 1).ToString() + ":") && !string.IsNullOrEmpty(DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].Where(www => ((www.Length - www.Replace(formingWord, String.Empty).Length) / formingWord.Length) > 0).FirstOrDefault()))
                FindWords_Recursive(i - 1, j + 1, formingWord + Matrix[i - 1, j + 1], visitedPath + (i - 1).ToString() + (j + 1).ToString() + ":");
            if (i - 1 > -1 && j + 0 < 4 && !visitedPath.Contains((i - 1).ToString() + (j + 0).ToString() + ":") && !string.IsNullOrEmpty(DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].Where(www => ((www.Length - www.Replace(formingWord, String.Empty).Length) / formingWord.Length) > 0).FirstOrDefault()))
                FindWords_Recursive(i - 1, j + 0, formingWord + Matrix[i - 1, j + 0], visitedPath + (i - 1).ToString() + (j + 0).ToString() + ":");
            if (i - 1 > -1 && j - 1 > -1 && !visitedPath.Contains((i - 1).ToString() + (j - 1).ToString() + ":") && !string.IsNullOrEmpty(DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].Where(www => ((www.Length - www.Replace(formingWord, String.Empty).Length) / formingWord.Length) > 0).FirstOrDefault()))
                FindWords_Recursive(i - 1, j - 1, formingWord + Matrix[i - 1, j - 1], visitedPath + (i - 1).ToString() + (j - 1).ToString() + ":");
            if (i + 0 < 4 && j - 1 > -1 && !visitedPath.Contains((i + 0).ToString() + (j - 1).ToString() + ":") && !string.IsNullOrEmpty(DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].Where(www => ((www.Length - www.Replace(formingWord, String.Empty).Length) / formingWord.Length) > 0).FirstOrDefault()))
                FindWords_Recursive(i + 0, j - 1, formingWord + Matrix[i + 0, j - 1], visitedPath + (i + 0).ToString() + (j - 1).ToString() + ":");
            if (i + 1 < 4 && j - 1 > -1 && !visitedPath.Contains((i + 1).ToString() + (j - 1).ToString() + ":") && !string.IsNullOrEmpty(DictionariesAtoZ[Convert.ToInt32(formingWord[0] - 65)].Where(www => ((www.Length - www.Replace(formingWord, String.Empty).Length) / formingWord.Length) > 0).FirstOrDefault()))
                FindWords_Recursive(i + 1, j - 1, formingWord + Matrix[i + 1, j - 1], visitedPath + (i + 1).ToString() + (j - 1).ToString() + ":");

        }
    }
}
