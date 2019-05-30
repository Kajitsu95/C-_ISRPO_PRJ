using System;
using System.Collections.Generic;

namespace Airport
{
    class Program
    {
        static void Main(string[] args)
        {
            // список рейсов
            List<Flight> flights = new List<Flight>();

            // фильтр 
            Filter filter = new Filter();
            // инициализация фильтра
            filter.ClearFiltFlags();

            // обновление интерфейса
            ConsoleKeyInfo pressed;

            // меню
            do
            {
                // вывод меню
                Console.WriteLine("_________________________");
                Console.WriteLine("Выберите действие");
                Console.WriteLine($"1 - Добавить авиарейс");
                Console.WriteLine($"2 - Вывести все авиарейсы");
                Console.WriteLine($"3 - Вывести отфильтрованные авиарейсы");
                Console.WriteLine($"4 - Установить значения фильтра");
                Console.WriteLine($"Esc - Выйти");

                // выбор пункта меню
                pressed = Console.ReadKey();

                // выполнить выбранный пункт
                switch (pressed.Key)
                {
                    case ConsoleKey.D1: // добавление рейса
                        Flight.AddFlight(flights);
                        break;

                    case ConsoleKey.D2: // вывод всех рейсов
                        Flight.OutputFlights(ref flights);
                        break;

                    case ConsoleKey.D3: // вывод отфильтрованного списка рейсов
                        Flight.FilterOut(flights, filter);
                        break;

                    case ConsoleKey.D4: // установка значений фильтра
                        filter.FiltFromConsole();
                        break;
                }
            } // условие выхода из главного меню
            while (pressed.Key != ConsoleKey.Escape);
        }

        // рейс
        struct Flight
        {
            private int Number;             // номер авиарейса
            private DateTime DepartureTime; // время вылета
            private DateTime ArrivalTime;   // время прилета
            private string Direction;       // направление
            private string AircraftMark;    // модель самолета
            private int Distance;           // расстояние

            /// <summary>
            /// инициализация рейса
            /// </summary>
            /// <param name="Number"> номер авиарейса</param>
            /// <param name="DepartureTime"> время вылета</param>
            /// <param name="ArrivalTime"> время прилета</param>
            /// <param name="Direction"> направление</param>
            /// <param name="AircraftMark"> модель самолета</param>
            /// <param name="Distance"> расстояние</param>
            public Flight(int Number, DateTime DepartureTime, DateTime ArrivalTime, string Direction, string AircraftMark, int Distance)
            {
                this.Number = Number;
                this.DepartureTime = DepartureTime;
                this.ArrivalTime = ArrivalTime;
                this.Direction = Direction;
                this.AircraftMark = AircraftMark;
                this.Distance = Distance;
            }

            // вывод одного рейса
            public void OutputFlight()
            {
                Console.WriteLine("Номер авиарейса: " + Number);
                Console.WriteLine("Дата и время вылета: " + DepartureTime);
                Console.WriteLine("Дата и время прилета: " + ArrivalTime);
                Console.WriteLine("Направление: " + Direction);
                Console.WriteLine("Марка самолёта: " + AircraftMark);
                Console.WriteLine("Расстояние: " + Distance);
                Console.WriteLine("_________________________");
            }

            /// <summary>
            /// добавление нового рейса в список рейсов
            /// </summary>
            /// <param name="flights"> список рейсов</param>
            public static void AddFlight(List<Flight> flights)
            {
                // попытка добавления нового рейса:
                try
                {
                    // запись информации о рейсе:
                    // номер рейса                    
                    Console.WriteLine("_________________________");
                    Console.Write("Введите номер авиарейса: ");
                    int numb = Int32.Parse(Console.ReadLine());

                    // время вылета
                    Console.Write("Введите дату и время вылета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                    DateTime d_time = DateTime.Parse(Console.ReadLine());

                    // время прилёта
                    Console.Write("Введите дату и время прилета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                    DateTime a_time = DateTime.Parse(Console.ReadLine());

                    // направление
                    Console.Write("Введите направление ");
                    string dir = Console.ReadLine();

                    // марка самолёта
                    Console.Write("Введите марку самолёта: ");
                    string mark = Console.ReadLine();

                    // расстояние
                    Console.Write("Введите расстояние: ");
                    int dist = Int32.Parse(Console.ReadLine());

                    // добавление в список рейсов:
                    flights.Add(new Flight(numb, d_time, a_time, dir, mark, dist));

                }
                // обработка ошибки
                catch
                {
                    Console.WriteLine("Ошибка добавления! Данные не корректны");
                }
            }

            /// <summary>
            /// вывод списка рейсов
            /// </summary>
            /// <param name="flights"> список рейсов</param>
            public static void OutputFlights(ref List<Flight> fligts)
            {
                // заголовок
                Console.WriteLine("_________________________");
                Console.WriteLine("Список всех рейсов: ");

                // для каждого рейса из списка рейсов
                foreach (var fligt in fligts)
                {
                    // вывод информации о рейсе
                    fligt.OutputFlight();
                }
            }

            /// <summary> вывод отфильтрованных рейсов </summary> 
            /// <param name="fligts"> список рейсов</param> 
            /// <param name="filter"> фильтр</param>
            public static void FilterOut(List<Flight> flights, Filter filter)
            {
                // заголовок
                Console.WriteLine("_________________________");
                Console.WriteLine("Отфильтрованный список рейсов: ");

                // Для каждого рейса из списка рейсов
                foreach (var flight in flights)
                {
                    // проверка полей фильтра:
                    // минимального номера рейса 
                    if (filter.flagMinNumber && flight.Number <= filter.minNumber) continue;

                    // максимального номера рейса 
                    if (filter.flagMaxNumber && flight.Number >= filter.maxNumber) continue;

                    // минимального времени вылета 
                    if (filter.flagMinDeparDate && flight.DepartureTime <= filter.DeparTimeMin) continue;

                    // максимального времени вылета 
                    if (filter.flagMaxDeparDate && flight.DepartureTime >= filter.DeparTimeMax) continue;

                    // минимального времени прилета 
                    if (filter.flagMinArrDate && flight.ArrivalTime <= filter.ArrTimeMin) continue;

                    // максимального времени прилета 
                    if (filter.flagMaxArrDate && flight.ArrivalTime >= filter.ArrTimeMax) continue;

                    // направления полета 
                    if (filter.flagDirection && !flight.Direction.Contains(filter.Direction)) continue;

                    // марки самолета 
                    if (filter.flagMark && !flight.AircraftMark.Contains(filter.AircraftMFilt)) continue;

                    // минимального расстояния 
                    if (filter.flagMinDistance && flight.Distance <= filter.minDistance) continue;

                    // максимального расстояния 
                    if (filter.flagMaxDistance && flight.Distance >= filter.maxDistance) continue;

                    // вывод отфильтрованного рейса 
                    flight.OutputFlight();
                }
            }
        }

        // фильтр рейсов 
        struct Filter
        {
            public int minNumber, maxNumber;                // значение фильтра для поля "Номер Авиарейса" 
            public DateTime DeparTimeMin, DeparTimeMax;     // значение фильтра для поля "Время вылета" 
            public DateTime ArrTimeMin, ArrTimeMax;         // значение фильтра для поля "Время прилета" 
            public string Direction;                        // значение фильтра для поля "Направление" 
            public string AircraftMFilt;                    // значение фильтра для поля "Марка самолета" 
            public int minDistance, maxDistance;            // значение фильтра для поля "Расстояние"

            public bool flagMinNumber;          // флаг использования фильтрации по значению: минимальный номер рейса
            public bool flagMaxNumber;          // флаг использования фильтрации по значению: максимальный номер рейса
            public bool flagMinDeparDate;       // флаг использования фильтрации по значению: минимальная дата вылета
            public bool flagMaxDeparDate;       // флаг использования фильтрации по значению: максимальная дата вылета
            public bool flagMinArrDate;         // флаг использования фильтрации по значению: минимальная дата прилета
            public bool flagMaxArrDate;         // флаг использования фильтрации по значению: максимальная дата прилета
            public bool flagDirection;          // флаг использования фильтрации по значению: направление полета
            public bool flagMark;               // флаг использования фильтрации по значению: марка Самолета
            public bool flagMinDistance;        // флаг использования фильтрации по значению: минимальное расстояние полета
            public bool flagMaxDistance;        // флаг использования фильтрации по значению: максимальное расстояние полета

            // очистка всех флагов использования фильтрации
            public void ClearFiltFlags()
            {
                flagMinNumber = false;
                flagMaxNumber = false;
                flagMinDeparDate = false;
                flagMaxDeparDate = false;
                flagMinArrDate = false;
                flagMaxArrDate = false;
                flagDirection = false;
                flagMark = false;
                flagMinDistance = false;
                flagMaxDistance = false;
            }

            // установка значений фильтра 
            public void FiltFromConsole()
            {
                // обновление интерфейса
                ConsoleKeyInfo pressed;
                Console.WriteLine();

                // подменю фильтра
                do
                {
                    // вывод подменю фильтра
                    Console.WriteLine("_________________________");
                    Console.WriteLine("Выбор поля фильтра");
                    Console.WriteLine($"1 - Минимальный номер рейса (используется - {flagMinNumber}, значение - {minNumber})");
                    Console.WriteLine($"2 - Максимальный номер рейса (используется - {flagMaxNumber}, значение - {maxNumber})");
                    Console.WriteLine($"3 - Минимальная дата вылета (используется - {flagMinDeparDate}, значение - {DeparTimeMin})");
                    Console.WriteLine($"4 - Максимальная дата вылета (используется - {flagMaxDeparDate}, значение - {DeparTimeMax})");
                    Console.WriteLine($"5 - Минимальная дата прилета (используется - {flagMinArrDate}, значение - {ArrTimeMin})");
                    Console.WriteLine($"6 - Максимальная дата прилета (используется - {flagMaxArrDate}, значение - {ArrTimeMax})");
                    Console.WriteLine($"7 - Направление полета (используется - {flagDirection}, значение - {Direction})");
                    Console.WriteLine($"8 - Марка Самолета (используется - {flagMark}, значение - {AircraftMFilt})");
                    Console.WriteLine($"9 - Минимальное расстояние полета (используется - {flagMinDistance}, значение - {minDistance})");
                    Console.WriteLine($"0 - Максимальное расстояние полета (используется - {flagMaxDistance}, значение - {maxDistance})");
                    Console.WriteLine($"Delete - Обнулить фильтр");
                    Console.WriteLine("Для выхода нажмите Escape");

                    // выбор пункта подменю
                    pressed = Console.ReadKey();
                    Console.WriteLine();

                    // обработка выбора пункта подменю
                    switch (pressed.Key)
                    {
                        case ConsoleKey.D1: // установка минимального значения фильтра по номеру рейса
                            Console.Write("Введите значение: ");
                            minNumber = Int32.Parse(Console.ReadLine());
                            flagMinNumber = true;
                            break;

                        case ConsoleKey.D2: // установка максимального значения фильтра по номеру рейса 
                            Console.Write("Введите значение: ");
                            maxNumber = Int32.Parse(Console.ReadLine());
                            flagMaxNumber = true;
                            break;

                        case ConsoleKey.D3: // установка минимального значения фильтра по дате вылета 
                            Console.Write("Введите минимальную дату вылета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                            DeparTimeMin = DateTime.Parse(Console.ReadLine());
                            flagMinDeparDate = true;
                            break;

                        case ConsoleKey.D4: // установка максимального значения фильтра по дате вылета 
                            Console.Write("Введите максимальную дату вылета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                            DeparTimeMax = DateTime.Parse(Console.ReadLine());
                            flagMaxDeparDate = true;
                            break;

                        case ConsoleKey.D5: // установка минимального значения фильтра по дате прилета 
                            Console.Write("Введите минимальную дату прилета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                            ArrTimeMin = DateTime.Parse(Console.ReadLine());
                            flagMinArrDate = true;
                            break;

                        case ConsoleKey.D6: // установка мамксимального значения фильтра по дате прилета
                            Console.Write("Введите максимальную дату прилета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                            ArrTimeMax = DateTime.Parse(Console.ReadLine());
                            flagMaxArrDate = true;
                            break;

                        case ConsoleKey.D7: // установка значения фильтра по направлению полета 
                            Console.Write("Введите направление полета: ");
                            Direction = Console.ReadLine();
                            flagDirection = true;
                            break;

                        case ConsoleKey.D8: // установка значения фильтра по марке самолета 
                            Console.Write("Введите марку самолета: ");
                            AircraftMFilt = Console.ReadLine();
                            flagMark = true;
                            break;

                        case ConsoleKey.D9: // установка минимального значения фильтра по расстоянию
                            Console.Write("Введите миинимальное расстояние полета: ");
                            minDistance = Int32.Parse(Console.ReadLine());
                            flagMinDistance = true;
                            break;

                        case ConsoleKey.D0: // установка максимального значения фильтра по расстоянию
                            Console.Write("Введите миинимальное расстояние полета: ");
                            maxDistance = Int32.Parse(Console.ReadLine());
                            flagMaxDistance = true;
                            break;

                        case ConsoleKey.Delete: // обнуление фильтра
                            ClearFiltFlags();
                            break;
                    }
                }// условие выхода из подменю
                while (pressed.Key != ConsoleKey.Escape);
            }
        }
    }
}
