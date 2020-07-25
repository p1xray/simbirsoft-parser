using System.Collections.Generic;
using System.Linq;

namespace Parser.Morphy
{
    /// <summary>
    /// Класс предназначен для работы и морфологической обработки текста.
    /// </summary>
    class MorphyService : IMorphyService<string>
    {
        IMorphySettings _settings;
        Dictionary<string, int> _result;

        public MorphyService(IMorphySettings settings)
        {
            _settings = settings;
            _result = new Dictionary<string, int>();
        }

        /// <summary>
        /// Метод подсчитывает количество уникальных слов в переданном тексте.
        /// </summary>
        /// <param name="text">Текст, в котором нужно посчитать количество уникальных слов.</param>
        /// <returns>Возвращает словарь, в котором ключ - это уникальное слово, а значение - количество его повторений в тексте.</returns>
        public Dictionary<string, int> CountingUniqueWords(string text)
        {
            string[] words = WordSplitting(text);

            // Фильтруем слова, чтобы не попадались различные пустые строки или знаки пунктуации.
            string[] filteredWords = FilteringWords(words);

            // Выделение уникальных слов.
            string[] uniqueWords = filteredWords.Select(q => q.ToUpper().Trim()).Distinct().ToArray();

            // Подсчет уникальных слов.
            foreach (var word in uniqueWords)
                _result.Add(word, filteredWords.Count(q => q.ToUpper().Equals(word)));

            return _result;
        }

        private string[] WordSplitting(string text)
        {
            return text.Split(_settings.WordSeparators);
        }

        private string[] FilteringWords(string[] words)
        {
            var filteredWords = new List<string>();

            foreach(string word in words)
            {
                string trimedWord = word.Trim();

                if (string.IsNullOrEmpty(trimedWord) ||
                    trimedWord == " " ||
                    trimedWord == "-")
                    continue;

                filteredWords.Add(trimedWord);
            }

            return filteredWords.ToArray();
        }
    }
}
