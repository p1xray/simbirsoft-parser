using System.Data.Entity;

namespace Parser.Database
{
    /// <summary>
    /// Класс предназначен для подключения и формирования таблиц в базе данных.
    /// </summary>
    class ParserDbContext : DbContext
    {
        public ParserDbContext() : base("DefaultConnection") { }

        // Формирование таблицы для хранения уникальных слов в базе данных.
        public DbSet<UniqueWordModel> UniqueWords { get; set; }
    }
}
