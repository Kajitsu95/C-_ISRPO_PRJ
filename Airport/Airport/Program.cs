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
            
            // обновление интерфейса
            ConsoleKeyInfo pressed;
            Console.Clear();
            
            // меню
            do
            {   
                // вывод меню
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
                // обновление интерфейса
                Console.Clear();
            }
            // условие выхода из главного меню
            while (pressed.Key != ConsoleKey.Escape);
        }

        // рейс
        struct Flight
        {
            private int Number;             // номер авиарейса
            private DateTime DepartureTime; // время вылета
            private DateTime ArrivalTime;   // время прилета
            private string Direction;       // направление
            private string AircraftMark;    // марка самолета
            private int Distance;           // расстояние

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

            //  добавление нового рейса в список рейсов
            public static void AddFlight(List<Flight> flights)
            {
                // попытка добавления нового рейса:
                try
                {
                    // обновление интерфейса
                    Console.Clear();

                    // запись информации о рейсе:
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

                    // добавление в список рейсов:
                    flights.Add(new Flight(numb, d_time, a_time, dir, mark, dist));

                }
                // обработка ошибки
                catch
                {
                    Console.WriteLine("Ошибка добавления! Данные не корректны");
                }
            }

            // вывод списка рейсов
            public static void OutputFlights(ref List<Flight> fligts)
            {     
                // обновление интерфейса
                Console.Clear();
                ConsoleKeyInfo pressed; 
             
                // меню
                do
                {
                    // заголовок меню
                    Console.WriteLine("Список всех рейсов: ");

                    // для каждого рейса из списка рейсов
                    foreach (var fligt in fligts)
                    {
                        // вывод информации о рейсе
                        fligt.OutputFlight();
                    }
                    //выход из меню
                    Console.WriteLine("Для выхода нажмите Enter");                   
                    pressed = Console.ReadKey();
                } 
                // условие выхода из меню
                while (pressed.Key != ConsoleKey.Enter);
            }
            
            // вывод отфильтрованных рейсов
            public static void FilterOut(List<Flight> flights, Filter filter)
            {
                // обновление интерфейса
                Console.Clear();
                ConsoleKeyInfo pressed;

                // вывод меню:
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

                        // вывод отфильтрованного рейса 
                        flight.OutputFlight();
                    }
                       // выход из подменю
                    Console.WriteLine("Для выхода нажмите Enter");
                    pressed = Console.ReadKey();
                } 
                // условие выхода из подменю
                while (pressed.Key != ConsoleKey.Enter);
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

            // установка значений фильтра 
            public void FiltFromConsole()
            {
                // вывод подменю фильтра
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

                // обработка выбора пункта меню
                switch (Console.ReadLine())
                {
                 
                    case "1": // установка минимального значения фильтра по номеру рейса 
                        Console.Write("Введите значение: ");
                        minNumber = Int32.Parse(Console.ReadLine());
                        break;
                  
                    case "2": // установка максимального значения фильтра по номеру рейса 
                        Console.Write("Введите значение: ");
                        maxNumber = Int32.Parse(Console.ReadLine());
                        break;
                  
                    case "3": // установка минимального значения фильтра по дате вылета 
                        Console.Write("Введите минимальную дату вылета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                        DeparTimeMin = DateTime.Parse(Console.ReadLine());
                        break;
                  
                    case "4": // установка максимального значения фильтра по дате вылета 
                        Console.Write("Введите максимальную дату вылета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                        DeparTimeMax = DateTime.Parse(Console.ReadLine());
                        break;
                   
                    case "5": // установка минимального значения фильтра по дате прилета 
                        Console.Write("Введите минимальную дату прилета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                        ArrTimeMin = DateTime.Parse(Console.ReadLine());
                        break;
                   
                    case "6": //Установка мамксимального значения фильтра по дате прилета
                        Console.Write("Введите максимальную дату прилета строго в формате [ДД/ММ/ГГГГ HH:MM:SS]: ");
                        ArrTimeMax = DateTime.Parse(Console.ReadLine());
                        break;
                   
                    case "7": // установка значения фильтра по направлению полета 
                        Console.Write("Введите направление полета: ");
                        Direction = Console.ReadLine();
                        break;
                   
                    case "8": // установка значения фильтра по марке самолета 
                        Console.Write("Введите марку самолета: ");
                        AircraftMFilt = Console.ReadLine();
                        break;
                    
                    case "9": // установка минимального значения фильтра по расстоянию
                        Console.Write("Введите миинимальное расстояние полета: ");
                        minDistance = Int32.Parse(Console.ReadLine());
                        break;
                     
                    case "10": // установка максимального значения фильтра по расстоянию
                        Console.Write("Введите миинимальное расстояние полета: ");
                        maxDistance = Int32.Parse(Console.ReadLine());
                        break;
                }
            }
        }
    }
}
