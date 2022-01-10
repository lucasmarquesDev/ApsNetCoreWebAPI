using System;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime dateTime) //Realizado o override no tipo DateTime
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTime.Year;

            if (currentDate < dateTime.AddYears(age))
                age--;

            return age;
        }

        public static int GetCurrentJob(this DateTime datetime)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - datetime.Year;

            if (currentDate < datetime.AddYears(age))
                age--;

            return age;
        }
    }
}