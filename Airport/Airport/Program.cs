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
            filter.FiltFromConsole();

            // флаг нажатия клавиши
            ConsoleKeyInfo pressed;

            // удерживать меню:
            do
            {   
                // очистить консоль           
                Console.Clear();

                // вывести меню включающее в себя:
                Console.WriteLine("Выберите действие");
                // сообщение о возможности добавления рейса по клавише - 1
                Console.WriteLine($"1 - Добавить авиарейс");
                // сообщение о возможности вывода всех рейсов на экран по клавише - 2
                Console.WriteLine($"2 - Вывести все авиарейсы");
                // сообщение о возможности вывода отфильтрованного списка рейсов на экран по клавише - 3
                Console.WriteLine($"3 - Вывести отфильтрованные авиарейсы");
                // сообщение о возможности установки значений фильтра по клавише - 4
                Console.WriteLine($"4 - Установить значения фильтра");
                // сообщение о возможности выхода из программы по клавише - Esc
                Console.WriteLine($"Esc - Выйти");

                // прочитать нажатую клавишу
                pressed = Console.ReadKey();

                // смотря какая клавиша нажата, перейти к определённой функции
                switch (pressed.Key)
                {                   
                    case ConsoleKey.D1: // перейти к функции добавления рейса по клавише - 1
                        Flight.AddFlight(flights);
                        break;                      
                   
                    case ConsoleKey.D2: // перейти к функции вывода всех рейсов на экран по клавише - 2
                        Flight.OutputFlights(ref flights);
                        break;                       
                   
                    case ConsoleKey.D3: // перейти к функции вывода отфильтрованного списка рейсов на экран по клавише - 3
                        Flight.FilterOut(flights, filter);
                        break;                       
                   
                    case ConsoleKey.D4: // перейти к функции установки значений фильтра по клавише - 4
                        filter.FiltFromConsole();
                        break;
                }                
                Console.Clear(); // очистить консоль                
            } 
            while (pressed.Key != ConsoleKey.Escape); // пока не нажата клавиша Escape
        }

        // структура рейса
        struct Flight
        {
            private int Number;             // номер авиарейса
            private DateTime DepartureTime; // время вылета
            private DateTime ArrivalTime;   // время прилета
            private string Direction;       // направление
            private string AircraftMark;    // марка самолета
            private int Distance;           // расстояние

            // вывод одного рейса в консоль
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

            // метод добавление нового рейса в список рейсов
            public static void AddFlight(List<Flight> flights)
            {
                // попытка добавления нового рейса:
                try
                {
                    // очистить консоль
                    Console.Clear();

                    // чтение с клавиатуры и запись во временные переменные:
                    // номер рейса
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

                    // добавление в список:
                    flights.Add(new Flight(numb, d_time, a_time, dir, mark, dist));

                }
                // неудача:
                catch
                {
                    // сообщение об ошибке введённых данных
                    Console.WriteLine("Ошибка добавления! Данные не корректны");
                }
            }

            // вывод списка рейсов
            public static void OutputFlights(ref List<Flight> fligts)
            {
                // очистить консоль
                Console.Clear();

                // флаг нажатия клавиши
                ConsoleKeyInfo pressed;

                // удерживать меню:
                do
                {
                    // вывести в консоль заголовок
                    Console.WriteLine("Список всех рейсов: ");

                    // для каждого рейса из списка рейсов
                    foreach (var fligt in fligts)
                    {
                        // вывести информацию о рейсе
                        fligt.OutputFlight();
                    }
                    // сообщение о возможности выхода по кнопке Enter
                    Console.WriteLine("Для выхода нажмите Enter");

                    // считать нажатую кнопку
                    pressed = Console.ReadKey();

                    // завершить цикл только если нажата кнопка Enter
                } while (pressed.Key != ConsoleKey.Enter);
            }
            
            //вывод отфильтрованных рейсов
            public static void FilterOut(List<Flight> flights, Filter filter)
            {
                // очистить консоль
                Console.Clear();

                // флаг нажатия клавиши
                ConsoleKeyInfo pressed;

                // удерживать меню:
                do
                {
                    // вывести в консоль заголовок
                    Console.WriteLine("Список всех рейсов: ");

                    // Для каждого рейса из списка рейсов
                    foreach (var flight in flights)
                    {
                        // проверка полей фильтра:
                        // номера рейса 
                        if (filter.minNumber != 0 && flight.Number > filter.minNumber) continue;
                        // номера рейса 
                        if (filter.maxNumber != 0 && flight.Number < filter.maxNumber) continue;
                        // минимального времени вылета 
                        if (filter.DeparTimeMin != null && flight.DepartureTime > filter.DeparTimeMin) continue;
                        // максимального времени вылета 
                        if (filter.DeparTimeMax != null && flight.DepartureTime < filter.DeparTimeMax) continue;
                        // минимального времени прилета 
                        if (filter.ArrTimeMin != null && flight.ArrivalTime < filter.ArrTimeMin) continue;
                        // максимального времени прилета 
                        if (filter.ArrTimeMax != null && flight.ArrivalTime > filter.ArrTimeMax) continue;
                        // направления полета 
                        if (filter.Direction != "" && flight.Direction.Contains(filter.Direction)) continue;
                        // марки самолета 
                        if (filter.AircraftMFilt != "" && flight.AircraftMark.Contains(filter.AircraftMFilt)) continue;
                        // минимального расстояния 
                        if (filter.minDistance != 0 && flight.Distance < filter.minDistance) continue;
                        // максимального расстояния 
                        if (filter.maxDistance != 0 && flight.Distance > filter.maxDistance) continue;

                        //Вывод отфильтрованного рейса на экран 
                        flight.OutputFlight();
                    }

                    // сообщение о возможности выхода по кнопке Enter
                    Console.WriteLine("Для выхода нажмите Enter");

                    // считать нажатую кнопку
                    pressed = Console.ReadKey();
                    
                } while (pressed.Key != ConsoleKey.Enter); // завершить цикл только если нажата кнопка Enter
            }
        }

         
        // структура фильтра рейсов 
        struct Filter
        {
            public int minNumber, maxNumber;                // значение фильтра для поля "Номер Авиарейса" 
            public DateTime DeparTimeMin, DeparTimeMax;     // значение фильтра для поля "Время вылета" 
            public DateTime ArrTimeMin, ArrTimeMax;         // значение фильтра для поля "Время прилета" 
            public string Direction;                        // значение фильтра для поля "Направление" 
            public string AircraftMFilt;                    // значение фильтра для поля "Марка самолета" 
            public int minDistance, maxDistance;            // значение фильтра для поля "Расстояние" 

            //Установка значений фильтра 
            public void FiltFromConsole()
            {
                //меню действий по выбору поля фильтра
                //от пользователя требуется ввод числа от 1 до 10
                //иначе происходит выход в главное меню без фильтрации
                Console.Clear();
                Console.WriteLine("Выбор поля фильтра");
                Console.WriteLine($"1 - Минимальный номер рейса({minNumber})");
                Console.WriteLine($"2 - Максимальный номер рейса({maxNumber})");
                Console.WriteLine($"3 - Минимальная дата вылета({DeparTimeMin})");
                Console.WriteLine($"4 - Максимальная дата вылета({DeparTimeMax})");
                Console.WriteLine($"5 - Минимальная дата прилета({ArrTimeMin})");
                Console.WriteLine($"6 - Максимальная дата прилета({ArrTimeMax})");
                Console.WriteLine($"7 - Направление полета ({Direction})");
                Console.WriteLine($"8 - Марка Самолета({AircraftMFilt})");
                Console.WriteLine($"9 - Минимальное расстояние полета({minDistance})");
                Console.WriteLine($"10 -Максимальное расстояние полета({maxDistance})");
                Console.WriteLine("Любое другое значение для выхода в меню");

                //проверка значения с клавиатуры
                //в соответствии с ним устанавливается соответствуещее значение фильтра
                switch (Console.ReadLine())
                {
                 
                    case ("1"): // установка минимального значения фильтрации по номеру рейса 
                        Console.Write("Введите значение: ");
                        minNumber = Int32.Parse(Console.ReadLine());
                        break;
                  
                    case ("2"): // установка максимального значения фильтрации по номеру рейса 
                        Console.Write("Введите значение: ");
                        maxNumber = Int32.Parse(Console.ReadLine());
                        break;
                  
                    case ("3"): // установка минимального значения фильтрации по дате вылета 
                        Console.Write("Введите минимальную дату вылета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                        DeparTimeMin = DateTime.Parse(Console.ReadLine());
                        break;
                  
                    case ("4"): // установка максимального значения фильтрации по дате вылета 
                        Console.Write("Введите максимальную дату вылета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                        DeparTimeMax = DateTime.Parse(Console.ReadLine());
                        break;
                   
                    case ("5"): // установка минимального значения фильтрации по дате прилета 
                        Console.Write("Введите минимальную дату прилета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                        ArrTimeMin = DateTime.Parse(Console.ReadLine());
                        break;
                   
                    case ("6"): //Установка мамксимального значения фильтрации по дате прилета
                        Console.Write("Введите максимальную дату прилета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                        ArrTimeMax = DateTime.Parse(Console.ReadLine());
                        break;
                   
                    case ("7"): // установка значения фильтрации по направлению полета 
                        Console.Write("Введите направление полета: ");
                        Direction = Console.ReadLine();
                        break;
                   
                    case ("8"): // установка значения фильтрации по марке самолета 
                        Console.Write("Введите марку самолета: ");
                        AircraftMFilt = Console.ReadLine();
                        break;
                    
                    case ("9"): // установка минимального значения фильтрации по расстоянию
                        Console.Write("Введите миинимальное расстояние полета: ");
                        minDistance = Int32.Parse(Console.ReadLine());
                        break;
                     
                    case ("10"): // установка максимального значения фильтрации по расстоянию
                        Console.Write("Введите миинимальное расстояние полета: ");
                        maxDistance = Int32.Parse(Console.ReadLine());
                        break;
                }
            }
        }
    }
}
