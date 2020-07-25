using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Parser.Database
{
    /// <summary>
    /// Класс предназначен для работы с базой данных.
    /// </summary>
    class UniqueWordsRepository : IRepository<UniqueWordModel>
    {
        #region Singleton
        private static UniqueWordsRepository _instance;
        public static UniqueWordsRepository Instance 
        { 
            get
            {
                if (_instance == null)
                    _instance = new UniqueWordsRepository();

                return _instance;
            }
        }
        #endregion

        private readonly ParserDbContext _context;

        public UniqueWordsRepository()
        {
            _context = new ParserDbContext();
        }

        /// <summary>
        /// Метод создает новую запись в базе данных модели уникального слова.
        /// </summary>
        /// <param name="model">Модель уникального слова.</param>
        public void Create(UniqueWordModel model)
        {
            SaveStateOfModel(model, EntityState.Added);
        }

        /// <summary>
        /// Метод обновляет запись в базе данных модели уникального слова.
        /// </summary>
        /// <param name="model">Модель уникального слова.</param>
        public void Update(UniqueWordModel model)
        {
            SaveStateOfModel(model, EntityState.Modified);
        }

        /// <summary>
        /// Метод удаляет запись в базе данных модели уникального слова.
        /// </summary>
        /// <param name="model">Модель уникального слова.</param>
        public void Delete(UniqueWordModel model)
        {
            _context.UniqueWords.Remove(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Метод ищет запись модели уникального слова из базы данных по определенному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи в базе данных.</param>
        /// <returns>Модель уникального слова с указанным идентификатором.</returns>
        public UniqueWordModel GetById(int id)
        {
            return _context.UniqueWords.SingleOrDefault(sor => sor.Id == id);
        }

        /// <summary>
        /// Метод ищет все записи моделей уникального слова из базы данных по определенному url-адресу.
        /// </summary>
        /// <param name="url">Url-адрес, по которому необходимо искать записи в базе данных.</param>
        /// <returns>Перечисление моделейе уникального слова с указанным url-адресом.</returns>
        public IEnumerable<UniqueWordModel> GetByUrl(string url)
        {
            return _context.UniqueWords.Where(sm => sm.Url == url);
        }

        private void SaveStateOfModel(UniqueWordModel model, EntityState state)
        {
            _context.Entry(model).State = state;
            _context.SaveChanges();
        }
    }
}
