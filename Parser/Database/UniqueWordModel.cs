using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Database
{
    /// <summary>
    /// Класс предназначен для предоставления модели записи в таблице базы данных.
    /// </summary>
    class UniqueWordModel
    {
        // Идентификатор.
        public int Id { get; set; }
        // Url-адрес html-страницы, на котором считались уникальные слова.
        public string Url { get; set; }
        // Уникальное слово.
        public string UniqueWord { get; set; }
        // Количество повторений данного уникального слова на странице.
        public int RepeatsNumber { get; set; }
    }
}
