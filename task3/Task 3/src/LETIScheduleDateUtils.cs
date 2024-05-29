namespace Task_3
{
    public static class LETIScheduleDateUtils
    {
        public static DateTime Today => DateTime.Today;

        public static DateTime Next {
            get {
                int offset = _daysOfWeekMap[TodayDayOfWeek].NextDayOffset;
                return Today.AddDays(offset);
            }
        }
        
        public static string TodayDayOfWeek => DayOfWeek(Today);
        public static string NextDayOfWeek => _daysOfWeekMap[TodayDayOfWeek].NextName;

        public static long TodaySeconds => (long) DateTime.Now.TimeOfDay.TotalSeconds;

        public static DateTime SemesterStartDay { get; set; }
        public static DateTime SemesterEndDay { get; set; }

        public static int TodayWeekOfYear => WeekOfYear(Today);
        public static int NextDayWeekOfYear => WeekOfYear(Next);

        public static int SemsterStartWeekOfYear => WeekOfYear(SemesterStartDay);
        public static int SemsterEndWeekOfYear => WeekOfYear(SemesterEndDay);

        public static int TodayStudyWeek => (TodayWeekOfYear - SemsterStartWeekOfYear) % 2 + 1;
        public static int NextDayStudyWeek => (NextDayWeekOfYear - SemsterStartWeekOfYear) % 2 + 1;

        public static List<string> DaysOfWeek => [
            "monday",
            "tuesday",
            "wednesday",
            "thursday",
            "friday",
            "saturday",
            "sunday"
        ];

        private record DayOfWeekInfo(int Index, string ApiName, string NextName, int NextDayOffset);

        private static readonly Dictionary<string, DayOfWeekInfo> _daysOfWeekMap = new()
        {
            {"monday"   , new DayOfWeekInfo(0, "MON", "tuesday"  , 1)},
            {"tuesday"  , new DayOfWeekInfo(1, "TUE", "wednesday", 1)},
            {"wednesday", new DayOfWeekInfo(2, "WED", "thursday" , 1)},
            {"thursday" , new DayOfWeekInfo(3, "THU", "friday"   , 1)},
            {"friday"   , new DayOfWeekInfo(4, "FRI", "saturday" , 1)},
            {"saturday" , new DayOfWeekInfo(5, "SAT", "monday"   , 2)},
            {"sunday"   , new DayOfWeekInfo(6, "SUN", "monday"   , 1)}  
        };

        public static string DayOfWeek(DateTime date)
        {
            return date.DayOfWeek.ToString().ToLower();
        }

        public static string DayOfWeekApi(string dayOfWeek)
        {
            return _daysOfWeekMap[dayOfWeek].ApiName;
        }

        public static int DayOfWeekIndex(string dayOfWeek)
        {
            return _daysOfWeekMap[dayOfWeek].Index;
        }

        public static int WeekOfYear(DateTime date)
        {
            return System.Globalization.ISOWeek.GetWeekOfYear(date);
        }

        public static bool ValidateDay(string day)
        {
            return _daysOfWeekMap.ContainsKey(day);
        }
        
    }   
}