using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    class Program
    {
         struct Airport
        {
            private int Number;//Номер Авиарейса
            private DateTime DepartureTime;//Время Вылета
            private DateTime ArrivalTime;//Время Прилета
            private string direction;//Направление
            private string AircraftMark;//Марка Самолета
            private int distance;//Расстояние

            public Airport(int Number, DateTime DepartureTime, DateTime ArrivalTime, string direction, string AircraftMark, int distance)
            {
                this.Number = Number;
                this.DepartureTime = DepartureTime;
                this.ArrivalTime = ArrivalTime;
                this.direction = direction;
                this.AircraftMark = AircraftMark;
                this.distance = distance;
            }
         }
        struct Filter
        {
            public int minNumber, maxNumber;//Фильтр для поля Номер Авиарейса
            public DateTime DeparTimeFilt;//Фильтр для поля Время Вылета
            public DateTime ArrTimeFilt;//Фильтр для поля Время Прилета
            public string directionFilt;//Фильтр для поля Направление
            public string AircraftMFilt;//Фильтр для поля Марка Самолета
            public int mindistance, maxdistance;//Фильтр для поля Расстояние
        }
        static void Main(string[] args)
        {
        }
    }
}
