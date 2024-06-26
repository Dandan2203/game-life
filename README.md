# Game of Life

## Опис
Це реалізація гри "Життя"

## Функціонал
- Запуск/зупинка гри через інтерфейс.
- Регулювання швидкості гри.
- Внесення змін в стан клітин ручним способом (ЛКМ для створення клітини та ПКМ для видалення).
- Збереження і завантаження стану ігрового поля у текстовий файл.

## Принципи програмування
- **DRY:** Уникнення повторення коду в методі `Neighbours`.
- **KISS:** Просте правило переходу до наступного покоління в `NextGeneration`.
- **SOLID:** Використання Single Responsibility і Open/Closed Principles через розділення логіки гри та управління інтерфейсом.
- **YAGNI:** Імплементація лише необхідних функцій.
- **Composition Over Inheritance:** Композиція класу `Logic` в `Form1`.

## Патерни проєктування
- **Observer Pattern:** Використання таймера в `Form1` для оновлення ігрового поля.
- **Factory Method:** Конструктор класу `Logic` для ініціалізації ігрового поля.
- **Memento:** Реалізація функцій збереження та завантаження стану ігрового поля.