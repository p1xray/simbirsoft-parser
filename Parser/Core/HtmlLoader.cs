using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parser.Core
{
    /// <summary>
    /// Класс предназначен для подключения и корректного скачивания html-страницы.
    /// </summary>
    class HtmlLoader
    {
        public string url;

        private readonly HttpClient _client;
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;

        public HtmlLoader(IParserSettings settings)
        {
            _client = new HttpClient();
            // Индентификации на сайте, который парсится.
            _client.DefaultRequestHeaders.Add("User", "HtmlParser");
            _inputFilePath = settings.InputFilePath;
            _outputFilePath = settings.OutputFilePath;
        }

        /// <summary>
        /// Метод скачивает и сохраняет на жесткий диск код html-страницы по указанному url.
        /// </summary>
        /// <returns>Html-код в виде строки скачанной html-страницы.</returns>
        public async Task<string> GetSource()
        {
            // Получаем url адрес сайта из входного файла.
            GetInputUrl();

            // Получаем ответ с сайта.
            HttpResponseMessage responce = await _client.GetAsync(url);
            if (responce == null)
                throw new Exception($"Response from url: {url} is null");
            if (responce.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Response status code {responce.StatusCode}");

            // Сохранение скачанной странцы на жесткий диск.
            await SaveHtml(responce);

            return await responce.Content.ReadAsStringAsync();
        }

        private void GetInputUrl()
        {
            url = File.ReadAllText(_inputFilePath);
            if (string.IsNullOrEmpty(url))
            {
                Console.WriteLine("Не указан url адрес сайта.");
                Console.ReadKey();

                throw new Exception("Не указан url адрес сайта.");
            }
        }

        private async Task SaveHtml(HttpResponseMessage responce)
        {
            using (var responseStream = await responce.Content.ReadAsStreamAsync())
            {
                using (var outputFileStream = File.Create(_outputFilePath))
                {
                    await responseStream.CopyToAsync(outputFileStream);
                    outputFileStream.Flush();
                }
                
            }
        }
    }
}
