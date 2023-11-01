using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConvertSha256
{
    public class ConvertS256
    {
        public ConvertS256(ref string output, string dateTime)
        {
            DateTime parsedDateTime = DateTime.Parse(dateTime);
            parsedDateTime = parsedDateTime.AddMinutes(25);
            DateTime roundedDateTime = RoundToNearest30Minutes(parsedDateTime);

            string roundedDateTimeString = roundedDateTime.ToString("MM/dd/yyyy h:mm:ss tt");
            output = ComputeSHA256Hash(roundedDateTimeString);
        }
        //static DateTime RoundToNearest30Minutes(DateTime dateTime)
        //{
        //    int minutes = dateTime.Minute;
        //    int remainder = minutes % 30;
        //    if (remainder < 15)
        //    {
        //        minutes -= remainder;
        //    }
        //    else
        //    {
        //        minutes += (30 - remainder);
        //    }

        //    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, minutes, 0);
        //}
        //private DateTime RoundToNearest30Minutes(DateTime dateTime)
        //{
        //    int minutes = dateTime.Minute;
        //    int remainder = minutes % 30;

        //    if (remainder < 15)
        //    {
        //        // Round down to the nearest 30 minutes
        //        minutes -= remainder;
        //    }
        //    else
        //    {
        //        // Round up to the nearest 30 minutes
        //        minutes += (30 - remainder);

        //        // Check if minutes exceed 59, and adjust the hour accordingly
        //        if (minutes >= 60)
        //        {
        //            minutes = 0;
        //            dateTime = dateTime.AddHours(1);
        //        }
        //    }

        //    return new DateTime(
        //        dateTime.Year,
        //        dateTime.Month,
        //        dateTime.Day,
        //        dateTime.Hour,
        //        minutes,
        //        0, // Seconds
        //        dateTime.Kind); // Preserve DateTimeKind
        //}
        private DateTime RoundToNearest30Minutes(DateTime dateTime)
        {
            int minutes = dateTime.Minute;
            int remainder = minutes % 30;

            if (remainder != 0)
            {
                // Round up to the next 30 minutes
                minutes += (30 - remainder);

                // Check if minutes exceed 59, and adjust the hour accordingly
                if (minutes >= 60)
                {
                    minutes = 0;
                    dateTime = dateTime.AddHours(1);
                }
            }

            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                minutes,
                0, // Seconds
                dateTime.Kind); // Preserve DateTimeKind
        }

        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
