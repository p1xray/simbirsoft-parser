using System;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace Parser.Core
{
    /// <summary>
    /// Класс предназначен для работы загрузчика html-страницы и парсера.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого параметра парсером</typeparam>
    class ParserWorker<T> where T : class
    {
        private IParserSettings _parserSettings;
        private HtmlLoader _loader;
        public IParser<T> Parser { get; set; }
        public IParserSettings Settings
        {
            get { return _parserSettings; }
            set
            {
                _parserSettings = value; // Новые настройки парсера.
                _loader = new HtmlLoader(value); // Сюда помещаются настройки для загрузчика кода страницы.
            }
        }

        // Это событие отвечает за информирование при старте работы парсера.
        public event Action<object> OnStart;
        // Это событие отвечает за информирование при завершении работы парсера.
        public event Action<object, T> OnComplited;

        public ParserWorker(IParser<T> parser)
        {
            Parser = parser;
        }

        /// <summary>
        /// Метод получает html-код страницы и запускает работу парсера.
        /// </summary>
        public async void StartParse()
        {
            OnStart?.Invoke(this);

            // Получаем код страницы.
            string source = await _loader.GetSource();
            if(string.IsNullOrEmpty(source))
                return;

            // Парсим код страницы с помощью AngleSharp.
            var domParser = new AngleSharp.Html.Parser.HtmlParser();
            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
            T result = Parser.Parse(document);

            OnComplited?.Invoke(this, result);
        }

        /// <summary>
        /// Метод используется для получаения url-адреса, считанного из входного файла.
        /// </summary>
        /// <returns>Строка url-адреса.</returns>
        public string GetUrl()
        {
            return _loader.url;
        }
    }
}
