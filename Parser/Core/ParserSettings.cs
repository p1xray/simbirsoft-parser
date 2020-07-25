namespace Parser.Core
{
    /// <summary>
    /// Класс предназначен для установки настроек парсера.
    /// </summary>
    class ParserSettings : IParserSettings
    {
        // Путь до входного файла с url-адресом сайта (расширение .txt).
        public string InputFilePath { get; set; }
        // Путь до выходного файла, куда скачивается html-страница(расширение .html).
        public string OutputFilePath { get; set; }

        public ParserSettings(string inputPath, string outputhPath)
        {
            InputFilePath = inputPath;
            OutputFilePath = outputhPath;
        }
    }
}
