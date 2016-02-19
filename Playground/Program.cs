using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("First day of month {0}",FirstDayOfMonth(DateTime.Now.Date));
            Console.WriteLine("First day of month {0}", FirstBusinessDayOfMonth(DateTime.Now.Date));
            Console.WriteLine("First day of month {0}", FirstDayOfWeek(DateTime.Now.Date));
            Console.WriteLine("First day of month {0}", FirstBusinessDayOfWeek(DateTime.Now.Date));
            Console.WriteLine("First day of month {0}", LastDayOfMonth(DateTime.Now).Date);
            Console.WriteLine("First day of month {0}", LastBusinessDayOfMonth(DateTime.Now).Date);
        }

        static DateTime FirstDayOfMonth(DateTime today)
        {
            //datetime.Day ~ 1-31
            return today.AddDays(-(today.Day - 1));
        }

        static DateTime LastDayOfMonth(DateTime today)
        {
            return FirstDayOfMonth(today.AddMonths(1)).AddDays(-1);
        }
        
        static DateTime FirstBusinessDayOfMonth(DateTime today)
        {
            var firstOfMonth = FirstDayOfMonth(today);
            if (IsBusinessDay(firstOfMonth))
                return firstOfMonth;
            return GetNextBusinessDay(firstOfMonth);
        }

        static bool IsBusinessDay(DateTime date)
        {
            //TODO add holidays
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        static DateTime GetNextBusinessDay(DateTime date)
        {
            var tomorrow = date.AddDays(1);
            if (IsBusinessDay(tomorrow))
                return tomorrow;
            return GetNextBusinessDay(tomorrow);
        }

        static DateTime GetLastBusinessDay(DateTime date)
        {
            var yesterday = date.AddDays(-1);
            if (IsBusinessDay(yesterday))
                return yesterday;
            return GetNextBusinessDay(yesterday);
        }
        
        static DateTime LastBusinessDayOfMonth(DateTime today)
        {
            var lastOfMonth = LastDayOfMonth(today);
            if (IsBusinessDay(lastOfMonth))
                return lastOfMonth;
            return GetLastBusinessDay(lastOfMonth);
        }

        /// <summary>
        /// This is alwasy a sunday
        /// </summary>
        static DateTime FirstDayOfWeek(DateTime today)
        {
            return today.AddDays(-((int)today.DayOfWeek));
        }

        static DateTime FirstBusinessDayOfWeek(DateTime today)
        {
            //starts with sunday which can be ignored
            return GetNextBusinessDay(FirstDayOfWeek(today));
        }

        /// <summary>
        /// This is always a saturday
        /// </summary>
        static DateTime LastDayOfWeek(DateTime today)
        {
            return FirstDayOfWeek(today).AddDays(6);
        }

        static DateTime LastBusinessDayOfWeek(DateTime today)
        {
            //Starts at saturday which is definitely not a businessday
            return GetLastBusinessDay(LastDayOfWeek(today));
        }
    }
}
