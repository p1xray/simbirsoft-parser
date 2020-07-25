using System.Collections.Generic;

namespace Parser.Morphy
{
    interface IMorphyService<T> where T : class
    {
        Dictionary<T, int> CountingUniqueWords(T texts);
    }
}
