namespace Parser.Core
{
    interface IParserSettings
    {
        string InputFilePath { get; set; }
        string OutputFilePath { get; set; }
    }
}
