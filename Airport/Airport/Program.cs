using System;
using System.Collections.Generic;

namespace Airport
{
    class Program
    {
        /* 
        * Структура рейса
        * которая имеет такие поля как:
        * Время вылета
        * Время прилета
        * Направление
        * Марка самолета
        * Расстояние
        * Имеет конструктор для инициализации всех полей структуры
        * Включает в себя такие методы как:
        * Метод добавления нового рейса в список рейсов
        * Метод вывода всего списка рейсов
        * Метод вывода фильтрованого списка
        */
        struct Flight
        {
            private int Number;             // Номер авиарейса
            private DateTime DepartureTime; // Время вылета
            private DateTime ArrivalTime;   // Время прилета
            private string Direction;       // Направление
            private string AircraftMark;    // Марка самолета
            private int Distance;           // Расстояние

            ///<summary>Конструктор для инициализации всех полей структуры</summary>
            ///<param name="Number">номер авиарейса</param>
            ///<param name="DepartureTime">время вылета</param>
            ///<param name="ArrivalTime">время прилета</param>
            ///<param name="Direction">направление</param>
            ///<param name="AircraftMark">марка самолета</param>
            ///<param name="Distance">расстояние</param>
            ///
            public Flight(int Number, DateTime DepartureTime, DateTime ArrivalTime, string Direction, string AircraftMark, int Distance)
            {
                this.Number = Number;
                this.DepartureTime = DepartureTime;
                this.ArrivalTime = ArrivalTime;
                this.Direction = Direction;
                this.AircraftMark = AircraftMark;
                this.Distance = Distance;
            }

            ///<summary>Метод вывода одного рейса в консоль</summary>
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

            ///<summary>Метод добавления нового рейса в список рейсов</summary>
            ///<param name="fligts">список рейсов</param>
            ///<param name="flag">флаг успешности добавления</param>
            ///
            public static void AddFlight(List<Flight> flights, bool flag)
            {
                try
                {
                    // Чтение с клавиатуры и запись во временные переменные:

                    // Номер рейса
                    Console.Write("Введите номер авиарейса: ");
                    int numb = Int32.Parse(Console.ReadLine());

                    // Время вылета
                    Console.Write("Введите дату и время вылета строго в формате [ДД.ММ.ГГГГ HH:MM:SS]: ");
                    DateTime d_time = DateTime.Parse(Console.ReadLine());

                    // Время прилёта
                    Console.Write("Введите дату и время прилета строго в формате [ДД.ММ.ГГГГ HH:MM:SS]: ");
                    DateTime a_time = DateTime.Parse(Console.ReadLine());

                    // Направление
                    Console.Write("Введите направление ");
                    string dir = Console.ReadLine();

                    // Марка самолёта
                    Console.Write("Введите марку самолёта: ");
                    string mark = Console.ReadLine();

                    // Расстояние
                    Console.Write("Введите расстояние: ");
                    int dist = Int32.Parse(Console.ReadLine());

                    // Добавление в список:
                    flights.Add(new Flight(numb, d_time, a_time, dir, mark, dist));
                }
                catch
                {
                    Console.WriteLine("Ошибка добавления! Данные не корректны");
                    flag = false;
                    return;
                }
            }

            ///<summary>Метод списока рейсов</summary>
            ///<param name="fligts">список рейсов</param>
            /// 
            public static void OutputFlights(ref List<Flight> fligts)
            {
                // Для каждого рейса
                foreach (var fligt in fligts)
                {
                    // Вывести информацию о рейсе
                    fligt.OutputFlight();
                }
            }
            /// <summary>Вывод отфильтрованных рейсов </summary> 
            /// <param name="fligts"> Список рейсов</param> 
            /// <param name="filter"> Фильтр</param> 
            public static void FilterOut(List<Flight> flights,Filter filter) 
            { 
                Console.Clear(); 
                foreach(var flight in flights) 
                { 
                    //Проверка номера рейса 
                    if (filter.minNumber != 0 && flight.Number > filter.minNumber) continue; 
                    //Проверка номера рейса 
                    if (filter.maxNumber != 0 && flight.Number < filter.maxNumber) continue; 
                    //Проверка минимального времени вылета 
                    if (filter.DeparTimeMin != null && flight.DepartureTime > filter.DeparTimeMin) continue; 
                    //Проверка максимального времени вылета 
                    if (filter.DeparTimeMax != null && flight.DepartureTime < filter.DeparTimeMax) continue; 
                    //Проверка минимального времени прилета 
                    if (filter.ArrTimeMin != null && flight.ArrivalTime < filter.ArrTimeMin) continue; 
                    //Проверка максимального времени прилета 
                    if (filter.ArrTimeMax != null && flight.ArrivalTime > filter.ArrTimeMax) continue; 
                    //Проверка направления полета 
                    if (filter.direction != "" && flight.Direction.Contains(filter.direction)) continue; 
                    //Проверка марки самолета 
                    if (filter.AircraftMFilt != "" && flight.AircraftMark.Contains(filter.AircraftMFilt)) continue; 
                    //Проверка минимального расстояния 
                    if (filter.mindistance != 0 && flight.Distance < filter.mindistance) continue; 
                    //Проверка максимального расстояния 
                    if (filter.maxdistance != 0 && flight.Distance > filter.maxdistance) continue; 
                    
                    flight.OutputFlight();//Вывод отфильтрованного рейса на экран 
                } 
            } 
        } 
        
        /* 
        * Структура фильтра рейсов 
        * которая имеет такие поля как: 
        * Значение фильтра для поля "Номер Авиарейса" 
        * Значение фильтра для поля "Время вылета" 
        * Значение фильтра для поля "Время прилета" 
        * Значение фильтра для поля "Направление" 
        * Значение фильтра для поля "Марка самолета" 
        * Значение фильтра для поля "Расстояние" 
        * Имеет конструктор для инициализации всех полей структуры 
        * которая имеет такие методы как: 
        * Ввод значения введёного с клавиатуры типа Int 
        * Ввод значения введёного с клавиатуры типа DateTime 
        * Метод изменения значения фильтра 
        */ 
        struct Filter 
        {                                                                        
            public int minNumber, maxNumber; // Значение фильтра для поля "Номер Авиарейса" 
            public DateTime DeparTimeMin,DeparTimeMax; // Значение фильтра для поля "Время вылета" 
            public DateTime ArrTimeMin,ArrTimeMax; // Значение фильтра для поля "Время прилета" 
            public string direction; // Значение фильтра для поля "Направление" 
            public string AircraftMFilt; // Значение фильтра для поля "Марка самолета" 
            public int mindistance, maxdistance; // Значение фильтра для поля "Расстояние" 
            
            //Установка значений фильтра 
            public void FiltFromConsole() 
            { 
                ///меню действий по выбору поля фильтра
                ///от пользователя требуется ввод числа от 1 до 10
                ///иначе происходит выход из меню без фильтрации
                Console.Clear(); 
                Console.WriteLine("Выбор поля фильтра"); 
                Console.WriteLine($"1 - Минимальный номер рейса({minNumber})"); 
                Console.WriteLine($"2 - Максимальный номер рейса({maxNumber})"); 
                Console.WriteLine($"3 - Минимальная дата вылета{DeparTimeMin})"); 
                Console.WriteLine($"4 - Максимальная дата вылета({DeparTimeMax})"); 
                Console.WriteLine($"5 - Минимальная дата прилета({ArrTimeMin})"); 
                Console.WriteLine($"6 - Максимальная дата прилета({ArrTimeMax})"); 
                Console.WriteLine($"7 - Направление полета ({direction})"); 
                Console.WriteLine($"8 - Марка Самолета({AircraftMFilt})"); 
                Console.WriteLine($"9 - Минимальное расстояние полета({mindistance})"); 
                Console.WriteLine($"10 -Максимальное расстояние полета({maxdistance})"); 
                Console.WriteLine("Любое другое значение для выхода в меню");
                ///проверка значения с клавиатуры
                ///в соответствии с ним устанавливается соответствуещее значение фильтра
                switch (Console.ReadLine()) 
                { 
                    //установка минимального значения фильтрации по номеру рейса 
                    case ("1"): 
                    Console.Write("Введите значение "); 
                    minNumber = Int32.Parse(Console.ReadLine()); 
                    break; 
                    //установка максимального значения фильтрации по номеру рейса 
                    case ("2"): 
                    Console.Write("Введите значение "); 
                    maxNumber = Int32.Parse(Console.ReadLine()); 
                    break; 
                    //Установка минимального значения фильтрации по дате вылета 
                    case ("3"): 
                    Console.Write("Введите минимальную дату вылета"); 
                    DeparTimeMin = DateTime.Parse(Console.ReadLine()); 
                    break; 
                    //Установка максимального значения фильтрации по дате вылета 
                    case ("4"): 
                    Console.Write("Введите максимальную дату вылета"); 
                    DeparTimeMax = DateTime.Parse(Console.ReadLine()); 
                    break; 
                    //Установка минимального значения фильтрации по дате прилета 
                    case ("5"): 
                    Console.Write("Введите минимальную дату прилета"); 
                    ArrTimeMin = DateTime.Parse(Console.ReadLine()); 
                    break; 
                    //Установка мамксимального значения фильтрации по дате прилета 
                    case ("6"): 
                    Console.Write("Введите максимальную дату прилета"); 
                    ArrTimeMax = DateTime.Parse(Console.ReadLine()); 
                    break; 
                    //Установка значения фильтрации по направлению полета 
                    case ("7"): 
                    Console.Write("Введите направление полета"); 
                    direction = Console.ReadLine(); 
                    break; 
                    //Установка значения фильтрации по марке самолета 
                    case ("8"): 
                    Console.Write("Введите марку самолета"); 
                    AircraftMFilt = Console.ReadLine(); 
                    break; 
                    //Установка минимального значения фильтрации по расстоянию 
                    case ("9"): 
                    Console.Write("Введите миинимальное расстояние полета"); 
                    mindistance = Int32.Parse(Console.ReadLine()); 
                    break; 
                    //Установка максимального значения фильтрации по расстоянию 
                    case ("10"): 
                    Console.Write("Введите миинимальное расстояние полета"); 
                    maxdistance = Int32.Parse(Console.ReadLine()); 
                    break;
                } 
            } 
        } 
        static void Main(string[] args) 
        { 
            List<Flight> flights = new List<Flight>(); 
            
            // Фильтр 
            Filter filter = new Filter(); 
            filter.FiltFromConsole(); 
            
            do 
            { 
                Console.Clear(); 
                Console.WriteLine("Выберите действие"); 
                Console.WriteLine($"1 - Добавить авиарейс"); 
                Console.WriteLine($"2 - Вывести все авиарейсы"); 
                Console.WriteLine($"3 - Вывести отфильтрованные авиарейсы"); 
                Console.WriteLine($"4 - Установить значения фильтра"); 
                Console.WriteLine("Любое другое значение"); 
            } 
            while (true); 
        }
    }
}
