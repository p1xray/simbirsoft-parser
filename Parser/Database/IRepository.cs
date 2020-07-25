using System.Collections.Generic;

namespace Parser.Database
{
    interface IRepository<T> where T : class
    {
        void Create(T model);
        void Update(T model);
        void Delete(T model);
        T GetById(int id);
        IEnumerable<T> GetByUrl(string url);
    }
}
