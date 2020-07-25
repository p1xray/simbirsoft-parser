using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System.Collections.Generic;

namespace Parser.Core
{
    /// <summary>
    /// Класс предназначен для парсинга html-страниц.
    /// </summary>
    class HtmlParser : IParser<string>
    {
        /// <summary>
        /// Метод парсит переданную ему html-страницу, выделяя из html-кода текст, который видит пользователь на странице.
        /// </summary>
        /// <param name="htmlDocument">Html-документ, возвращенный после парсинга кода страницы библиотекой AngleSharp</param>
        /// <returns>Возвращает строку текста, который видит пользователь на html-странице.</returns>
        public string Parse(IHtmlDocument htmlDocument)
        {
            List<string> texts = new List<string>();

            // Получаем все html-элементы из документа.
            List<IElement> elements = GetAllElements(htmlDocument.Body);

            foreach(IElement element in elements)
            {
                // Не обращаем внимание на теги подключения css и js файлов
                if (element.TagName == "BODY" ||
                    element.TagName == "LINK" ||
                    element.TagName == "SCRIPT" ||
                    element.TagName == "NOSCRIPT" ||
                    element.TagName == "META" ||
                    element.TagName == "STYLE")
                    continue;

                // Получаем текст из тегов.
                string text = element.TextContent.Trim();
                if (string.IsNullOrEmpty(text))
                    continue;

                texts.Add(text);
            }

            return string.Join(" ", texts);
        }

        private List<IElement> GetAllElements(IElement element)
        {
            List<IElement> elements = new List<IElement> { element };

            foreach (var child in element.Children)
            {
                elements.AddRange(GetAllElements(child));
            }

            return elements;
        }
    }
}
