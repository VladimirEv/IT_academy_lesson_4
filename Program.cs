using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace ItAcademy_HomeWork_4
{
    class Program
    {

        static void Main(string[] args)
        {
            OurFileRead ourFileRead = new OurFileRead();
            ourFileRead.Info();
            ourFileRead.TextOurFileRead();
        }
    }


    public class OurFileRead
    {
        string Path = @".\Text.txt";
        string PathAW = @".\TextWrite.txt";
        string PathLS = @".\TextWrite2.txt";

        string PathMCL1 = @".\TextWriteMCL1.txt";
        string PathMCL2 = @".\TextWriteMCL2.txt";

        string PathShot = @".\PathShot.txt";

        string g;



        public void Info()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Hi. It's inctructions for using programm! Follow instructions!");
            Console.WriteLine("Instructions: ");
            Console.WriteLine("Enter '1' - you will all text file.");
            Console.WriteLine("Enter '2' - you will divide the text by sentences.");
            Console.WriteLine("Enter '3' - you will divide the text by words.");
            Console.WriteLine("Enter '4' - you will divide the text by punctuation symbol.");
            Console.WriteLine("Enter '5' - you will divide the text by words in albhadet number and write in file this words.");
            Console.WriteLine("Enter '6' - you will find the longest sentence from our text and write in file.");
            Console.WriteLine("Enter '7' - you will find common letter from our text and write in file.");
            Console.WriteLine("Enter '8' - you will find the shotest sentence from our text and write in file.");
            Console.WriteLine("Enter '9' - you the application will finish work. The END!!!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LET'S GO !!!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void TextOurFileRead()
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                //DFG.Add(sr.ReadToEnd());
                g = sr.ReadToEnd();
            }

            try
            {
                int a = Convert.ToInt32(Console.ReadLine());

                switch (a)
                {
                    case 1: ShowFile(); break;
                    case 2: DivideBYSentences(); break;
                    case 3: DivideBYWords(); break;
                    case 4: DividePunctuationSymbol(); break;
                    case 5: AlphabetWordOut(); break;
                    case 6: LongestSentenceInText(); break;
                    case 7: MostCommonLetterInText(); break;
                    case 8: ShotestSentenceInText(); break;
                    case 9: TheEnd(); break;
                    default: Console.WriteLine("Something go wrong!"); break;
                }
                if (a == 1 || a == 2 || a == 3 || a == 4 || a == 5 || a == 6 || a == 7 || a == 8)
                {
                    Info();
                    TextOurFileRead();
                }
                else
                {
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.WriteLine("You entered a wrong symbole. Please, try again. Thanks for understanding!!!");
            }

        }


        public void TheEnd()
        {
            Console.WriteLine("The End. Bye!!!");
        }


        public void ShowFile()
        {
            Console.WriteLine(g);
        }


        public void DivideBYSentences()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("______________________________________________");
            Console.WriteLine("DIVIDE THE TEXT BY SENTENCES !!!");
            Console.WriteLine("LET'S GO !!!");

            string[] sentences = Regex.Split(g, @"(?<=[\.!\?])\s+");

            foreach (string sentence in sentences)
            {
                Console.WriteLine(sentence);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DivideBYWords()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("______________________________________________");
            Console.WriteLine("DIVIDE THE TEXT BY WORDS !!!");
            Console.WriteLine("LET'S GO !!!");

            String[] sub = g.Split(' ', '.', ',', ';', ':', '!', '?', '"', '(', ')', '_');

            foreach (var item in sub)
            {
                Console.WriteLine($"WORD: {item}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DividePunctuationSymbol()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("______________________________________________");

            Console.WriteLine("DIVIDE THE TEXT BY PUNCTUATION SYMBOLS!!!");
            Console.WriteLine("LET'S GO !!!");

            for (int i = 0; i < g.Length; i++)
            {
                if (char.IsPunctuation(g[i])) Console.Write(g[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }


        public void AlphabetWordOut()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("______________________________________________");
            Console.WriteLine("DIVIDE THE TEXT ON WORDS AND ENTER IN ALPHABET NUMBER!!!");
            Console.WriteLine("LET'S GO !!!");

            char[] D = { ' ', ',', '.', ':', '!', '?', ';', ';', '-', '"','=', '*', '\'', '#', '_', '(', ')','\r', '\t','\n', '+','&','[',']','/','$','|','0','1','2','3','4','5',
                                     '6','7','8','9','~','%','<','>','@'};
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Enter the words: ");
            Console.ForegroundColor = ConsoleColor.White;

            var anyWord = Console.ReadLine();// Enter any word
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

            string[] source = g.Split(D, StringSplitOptions.RemoveEmptyEntries);
            int count = source.Count(x => x == anyWord);
            Console.WriteLine($"The number of a given word : {count}");
            if (!File.Exists(g))
            {
                List<string> wordsFromText = g.Split(D, StringSplitOptions.RemoveEmptyEntries).ToList();// Read our text
                wordsFromText.Sort();
                wordsFromText.Count();

                using (StreamWriter sw1 = new StreamWriter(PathAW, false, Encoding.Default))
                {
                    foreach (var x in wordsFromText)
                    {
                        sw1.Write($"|{x}");
                    }
                }

                var result = wordsFromText.GroupBy(x => x)// the number of use every words!!! 
                              .Where(x => x.Count() > 1)

                              .Select(x => new { Word = x.Key, Frequency = x.Count() });

                if (!File.Exists(g))
                {
                    foreach (var y in result)
                    {

                    }

                    using (StreamWriter sw2 = new StreamWriter(PathAW, false, Encoding.Default))
                    {
                        foreach (var x in result)
                        {
                            sw2.WriteLine(x);
                        }
                    }
                }

            }
        }

        public void LongestSentenceInText()
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("______________________________________________");
            Console.WriteLine("ENTER THE LONGEST SENTENCE FROM OUR TEXT!!!");
            Console.WriteLine("LET'S GO !!!");

            string[] lonSen = g.Split(new Char[] {'.','!','?','+','/',']','[','_','|','0', '1', '2', '3', '4', '5',
                                                                      '6', '7', '8', '9' }, StringSplitOptions.RemoveEmptyEntries);
            int maxls = 0, index = 0;
            for (int i = 0; i < lonSen.Length; i++)
            {
                if (lonSen[i].Length > maxls)
                {
                    maxls = lonSen[i].Length;
                    index = i;
                }
            }
            using (StreamWriter LS1 = new StreamWriter(PathLS, false, Encoding.Default))
            {
                foreach (var item in lonSen[index])
                {
                    LS1.Write(item);
                }

            }
        }

        public void MostCommonLetterInText()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("______________________________________________");
            Console.WriteLine("FIND MOST COMMON LETTER TEXT!!!");
            Console.WriteLine("LET'S GO !!!");

            var newTextWithoutSym = Regex.Replace(g, @"[\n\t\W_\d]", "");

            var countMCL = newTextWithoutSym.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;

            Console.WriteLine($"Most common letter: {countMCL}");

            using (StreamWriter mcl_1 = new StreamWriter(PathMCL1, false, Encoding.Default))
            {
                {
                    mcl_1.WriteLine($"Most common letter: {countMCL}");
                }
            }

            var newTextWithoutSym2 = Regex.Replace(g, @"[\n\t\W_\d]", "");
            var countMSL2 = newTextWithoutSym2.GroupBy(c => c)
                                   .OrderBy(c => c.Key)
                                   .ToDictionary(grp => grp.Key, grp => grp.Count());

            if (!File.Exists(g))
                foreach (var character in countMSL2)
                {
                    Console.WriteLine("{0} - {1}", character.Key, character.Value);
                }

            using (StreamWriter mcl_2 = new StreamWriter(PathMCL2, false, Encoding.Default))
            {
                foreach (var item in countMSL2)
                {
                    mcl_2.WriteLine(item);
                }
            }
        }

        public void ShotestSentenceInText()
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("______________________________________________");
            Console.WriteLine("ENTER THE SHOTEST SENTENCE FROM OUR TEXT!!!");
            Console.WriteLine("LET'S GO !!!");

            string[] ShotSen = g.Split(new Char[] {'.','!','?','+','/',']','[','_','|','0', '1', '2', '3', '4', '5', ':', ' ',
                                                                      '6', '7', '8', '9' }, StringSplitOptions.RemoveEmptyEntries);
            int maxShotls = 1000000, index = 0;
            for (int i = 0; i < ShotSen.Length; i++)
            {
                if (ShotSen[i].Length < maxShotls && ShotSen[i].Length != 0)
                {
                    maxShotls = ShotSen[i].Length;
                    index = i;
                }
            }
            using (StreamWriter SHOT1 = new StreamWriter(PathShot, false, Encoding.Default))
            {
                foreach (var item in ShotSen[index])
                {
                    SHOT1.Write(item);
                }

            }
        }
    }
}
