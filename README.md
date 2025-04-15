
# Описание экранов программы для базы данных весов

На основе предоставленного задания и созданной базы данных, я разработаю структуру экранов программы, которая будет соответствовать всем требованиям.

## Главное меню

**Экран 1: Главное меню**
- Кнопка "Аэродинамические трубы"
- Кнопка "Весы"
- Кнопка "Модели самолетов"
- Кнопка "Винты"
- Кнопка "Градуировки"
- Кнопка "Отчеты"


## Экран работы с аэродинамическими трубами

**Экран 2: Список аэродинамических труб**
- Таблица с колонками: ID, Название трубы
- Кнопки: Добавить
 
**Экран 3: Добавление/редактирование/удаление  трубы**
- Поле ввода: Название трубы (валидация на формат "АТ-NNN")
- Кнопки: Сохранить, Отмена, (Кнопка удалить если выбран режим изменение) 

## Экран работы с весами

**Экран 4: Список весов**
- Таблица с колонками: Название, Тип весов, Труба, Количество компонент, путь паспорта 
- Кнопки: Добавить
 

**Экран 5: Добавление/редактирование/(удаление если выбран режим изменение)   весов**
- Поля ввода:
  - Название (валидация по типу весов)
  - Тип весов (выпадающий список: внешние AX, внутримодельные AX, винтовые)
  - Аэродинамическая труба (выпадающий список)
  - Количество компонент (1-6, чекбоксы для выбора компонент X,Y,Z,MX,MY,MZ)
  - Путь к паспорту (PDF)
  - Путь к чертежу (PDF или изображение)
- Динамическая валидация названия в зависимости от типа весов
- Кнопки: Сохранить, Отмена

## Экран работы с моделями самолетов

**Экран 6: Список моделей**
- Таблица с колонками: ID, Название модели, Номер модели
- Кнопки: Добавить, Назад, привязать весы, Показать связь 

**Экран 7: Добавление/редактирование (удаление если выбран режим изменение) модели**
- Поля ввода:
  - Название модели (текстовое)
  - Номер модели (4-6 символов, буквы и цифры)
- Кнопки: Сохранить, Отмена, Удалить

**Экран 8: Привязка весов к модели**
- Выбор весов для аэродинамических характеристик (1 весы)
- Выбор весов для винтовых характеристик (можно несколько)
- Кнопки: Сохранить, Отмена

## Экран работы с винтами

Экран 10: Cписок винтов 
- Таблица с колонками: ID, Название винта 
- Кнопки: Добавить, Назад, привязать весы, Показать связь 
 	

**Экран 11: Привязка винтов к весам**
- Выбор весов винтовых характеристик
- Выбор винтов для привязки (можно несколько)
- Кнопки: Сохранить, Отмена

## Экран работы с градуировками

**Экран 12: Список градуировок для выбранных весов**
- Таблица с колонками: ID, Название градуировки, Дата
- Кнопки: Добавить, Экспорт 

**Экран 13: Добавление/редактирование/удаление градуировки**
- название 
- Таблица градуировки (10 строк, 6 колонок) с возможностью ввода чисел
- Валидация на рациональные числа (до 16 знаков)
- Кнопки: Сохранить, Отмена

**Экран 14: Просмотр градуировки**
- Отображение таблицы градуировки в читаемом формате
- Кнопка экспорта в TXT
- Кнопка Назад

## Отчеты и поиск

**Экран 15: Отчеты**
- Отчет по:
  - Весам по трубам
  - Градуировкам по датам
  - Моделям с привязанными весами
-Экспорт в PDF, Назад

 
 
