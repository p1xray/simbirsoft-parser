namespace Parser.Morphy
{
    /// <summary>
    /// Класс предназначен для установки настроек для класса MorphyService.
    /// </summary>
    class MorphySettings : IMorphySettings
    {
        // Массив разделителей для выделения отдельных слов из текста.
        public char[] WordSeparators { get; set; } = { ' ', ',', '.', '!', '?', '/', '\\', '"', '«', '»', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t'};
    }
}
