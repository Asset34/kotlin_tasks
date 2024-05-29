using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.VisualBasic;

namespace Task_3
{
    public static class LETIScheduleReceiver
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://digital.etu.ru/api/mobile/")
        };


        static LETIScheduleReceiver()
        {
            // Setup Semester Dates

            var semesterInfoTask = ReceiveCurrentSemesterInfoAsync();
            semesterInfoTask.Wait();

            LETIScheduleDateUtils.SemesterStartDay = DateTime.Parse(semesterInfoTask.Result.StartDate);
            LETIScheduleDateUtils.SemesterEndDay = DateTime.Parse(semesterInfoTask.Result.EndDate);
        }


        public static async Task<ScheduleDay?> ReceiveDayScheduleAsync(string group, string day, int week)
        {
            // Validate day
            if(!LETIScheduleDateUtils.ValidateDay(day))
            {
                return null;
            }

            // Make a request
            string query = MakeDayScheduleQuery(group, day);
            var jsonString = await _httpClient.GetStringAsync(query);
            
            // Navigate JSON
            var jsonNavigated = JsonDocument.Parse(jsonString).RootElement;
            if (!jsonNavigated.TryGetProperty(group, out jsonNavigated))
            {
                return null;
            }
            jsonNavigated = jsonNavigated.GetProperty("days");
            if (!jsonNavigated.TryGetProperty(LETIScheduleDateUtils.DayOfWeekIndex(day).ToString(), out jsonNavigated))
            {
                return null;
            }
            
            // Parse JSON
            ScheduleDay? result = JsonSerializer.Deserialize<ScheduleDay>(jsonNavigated);
            if (result is null)
            {
                return null;
            }

            // Filter result by week
            var filteredResult = 
                from subject in result.Subjects
                where subject.Week == week.ToString()
                select subject;
            result.Subjects = filteredResult.ToList(); 

            return result;
        }

        public static async Task<ScheduleDay?> ReceiveTodayScheduleAsync(string group)
        {
            return await ReceiveDayScheduleAsync(
                group,
                LETIScheduleDateUtils.TodayDayOfWeek,
                LETIScheduleDateUtils.TodayStudyWeek
            );
        }

        public static async Task<ScheduleDay?> ReceiveNextDayScheduleAsync(string group)
        {
            return await ReceiveDayScheduleAsync(
                group,
                LETIScheduleDateUtils.NextDayOfWeek,
                LETIScheduleDateUtils.NextDayStudyWeek
            );
        }

        public static async Task<Subject?> ReceiveClosestSubjectAsync(string group)
        {
            // Try today schedule
            ScheduleDay? scheduleDay = await ReceiveTodayScheduleAsync(group);
            if (scheduleDay is null) {
                return null;
            }

            // Find closest subject for current day
            Subject? closestSubject = scheduleDay.Subjects.Find(x => LETIScheduleDateUtils.TodaySeconds < x.EndTimeSeconds);

            // Check today for left subjects
            if (closestSubject is not null)
            {
                return closestSubject;
            }

            // Try next study day schedule
            scheduleDay = await ReceiveNextDayScheduleAsync(group);
            if (scheduleDay is null) {
                return null;
            }
            
            closestSubject = scheduleDay.Subjects.First();

            return closestSubject;
        }

        public static async Task<ScheduleWeek?> ReceiveWeekScheduleAsync(string group, int week)
        {
            ScheduleWeek scheduleWeek = new() { Week = week, DaySchedules = [] };

            foreach (string day in LETIScheduleDateUtils.DaysOfWeek)
            {
                ScheduleDay? daySchedule = await ReceiveDayScheduleAsync(group, day, week);
                if (daySchedule is null)
                {
                    return null;
                }

                scheduleWeek.DaySchedules.Add(daySchedule);
            }

            return scheduleWeek;
        }


        private static async Task<SemesterInfo?> ReceiveCurrentSemesterInfoAsync()
        {
            // Make a request
            string query = MakeCurrentSemesterQuery();
            var jsonString = await _httpClient.GetStringAsync(query);
            
            // Parse JSON
            SemesterInfo? result = JsonSerializer.Deserialize<SemesterInfo>(jsonString);

            return result;
        }

        private static string MakeDayScheduleQuery(string group, string day)
        {
            StringBuilder sb = new();

            sb.Append("schedule?");
            sb.Append($"groupNumber={group}");
            sb.Append($"&weekDay={LETIScheduleDateUtils.DayOfWeekApi(day)}");

            return sb.ToString();
        }

        private static string MakeWeekScheduleQuery(string group)
        {
            StringBuilder sb = new();

            sb.Append("schedule?");
            sb.Append($"groupNumber={group}");

            return sb.ToString();
        }

        private static string MakeCurrentSemesterQuery()
        {
            return "semester?season=current&year=current";
        }
    }
}