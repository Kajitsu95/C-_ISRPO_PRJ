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
            public int minNumber, maxNumber;     // Значение фильтра для поля "Номер Авиарейса"
            public DateTime DeparTimeFilt;       // Значение фильтра для поля "Время вылета"
            public DateTime ArrTimeFilt;         // Значение фильтра для поля "Время прилета"
            public string directionFilt;         // Значение фильтра для поля "Направление"
            public string AircraftMFilt;         // Значение фильтра для поля "Марка самолета"
            public int mindistance, maxdistance; // Значение фильтра для поля "Расстояние"
        }
        static void Main(string[] args)
        {
        }
    }
}
