using Sibnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sibnia.Services
{
    internal class Helpel
    {
        private static sibnia_practicaEntities _context;
        public static sibnia_practicaEntities GetContext() {

            // Проверка, установлено ли подключение; если нет, создается новое подключение
            if (_context == null)
            {
                _context = new sibnia_practicaEntities(); // Создание нового подключения к БД
            }
            return _context; // Воз

        }

    }
}
