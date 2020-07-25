using Parser.Core;
using Parser.Database;
using Parser.Morphy;
using System;
using System.Collections.Generic;

namespace Parser
{
    class Program
    {
        private static readonly ParserWorker<string> parser = new ParserWorker<string>(new HtmlParser());
        private static readonly MorphyService morphyService = new MorphyService(new MorphySettings());
        private static readonly UniqueWordsRepository uniqueWordsRepository = new UniqueWordsRepository();
        private static Dictionary<string, int> uniqueWordsCount = new Dictionary<string, int>();

        private static void Main()
        {
            parser.OnStart += ParserOnStart;
            parser.OnComplited += ParserOnComplited;
            parser.Settings = new ParserSettings("../../../IO/inputUrl.txt", "../../../IO/site.html");
            
            parser.StartParse();

            Console.ReadKey();
        }

        private static void ParserOnStart(object parser)
        {
            Console.WriteLine("Парсер анализирует страницу...");
        }

        private static void ParserOnComplited(object parser, string text)
        {
            Console.WriteLine("Работа парсера завершена.");
            CountingUniqueWords(text);
            SaveUniqueWordsToDb();
            PrintUniqueWords();
        }

        private static void CountingUniqueWords(string text)
        {
            Console.WriteLine("Подсчитывается количество повторений уникальных слов на странице...");
            uniqueWordsCount =  morphyService.CountingUniqueWords(text);
        }

        private static void PrintUniqueWords()
        {
            Console.WriteLine("Найденные уникальные слова на странице:");
            foreach (KeyValuePair<string, int> uniqueWord in uniqueWordsCount)
                Console.WriteLine(string.Format("{0} - {1}", uniqueWord.Key, uniqueWord.Value));
        }

        private static void SaveUniqueWordsToDb()
        {
            string url = parser.GetUrl();
            foreach(KeyValuePair<string, int> uniqueWord in uniqueWordsCount)
            {
                var model = new UniqueWordModel
                {
                    Url = url,
                    UniqueWord = uniqueWord.Key,
                    RepeatsNumber = uniqueWord.Value
                };

                uniqueWordsRepository.Create(model);
            }
            
        }
    }
}
