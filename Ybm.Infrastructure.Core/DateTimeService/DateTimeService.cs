using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Ybm.Infrastructure.Core.DateTimeService
{
    public class DateTimeService
    {

        public DateTime Now()
        {
            return DateTime.Now;
        }

        public string GetPersianDateWithTime(DateTime helper)
        {
            string format = "yyyy/mm/dd hh:jj:ss";

            if (helper.Year < 1000) helper = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();

            StringBuilder result = new StringBuilder(format.ToLower());

            result = result.Replace("hh", helper.Hour.ToString());
            result = result.Replace("jj", helper.Minute.ToString());
            result = result.Replace("ss", helper.Second.ToString());

            result = result.Replace("yyyy", pc.GetYear(helper).ToString());

            result = result.Replace("mm", pc.GetMonth(helper).ToString("00"));

            result = result.Replace("dd", pc.GetDayOfMonth(helper).ToString("00"));

            return result.ToString();
        }
        public string GetPersianDate(DateTime helper)
        {
            string format = "yyyy/mm/dd";

            if (helper.Year < 1000) helper = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();

            StringBuilder result = new StringBuilder(format.ToLower());

            result = result.Replace("yyyy", pc.GetYear(helper).ToString());

            result = result.Replace("mm", pc.GetMonth(helper).ToString("00"));

            result = result.Replace("dd", pc.GetDayOfMonth(helper).ToString("00"));

            return result.ToString();
        }

        public string GetPersianYearAndMonth(DateTime helper, string format = "yyyy/mm")
        {

            if (helper.Year < 1000) helper = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();

            StringBuilder result = new StringBuilder(format.ToLower());

            result = result.Replace("yyyy", pc.GetYear(helper).ToString());

            result = result.Replace("mm", pc.GetMonth(helper).ToString("00"));

            return result.ToString();
        }

        public DateTime GetGregorianDate(string persianDateTime)
        {
            string pattern = @"(?>((?>13|14)\d\d)|(\d\d))\/(0?[1-9]|1[012])\/([12][0-9]|3[01]|0?[1-9])";
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(persianDateTime, pattern))
                {
                    System.Text.RegularExpressions.Regex d = new System.Text.RegularExpressions.Regex(pattern);
                    int year = int.Parse(d.Match(persianDateTime).Groups[1].Value);
                    int month = int.Parse(d.Match(persianDateTime).Groups[3].Value);
                    int day = int.Parse(d.Match(persianDateTime).Groups[4].Value);
                    PersianCalendar result = new PersianCalendar();
                    return result.ToDateTime(year, month, day, 0, 0, 0, 0);
                }
            }
            catch (Exception)
            {
                return default(DateTime);
            }
            return default(DateTime);
        }

        public DateTime GetGregorianDateTime(string persianDateTime)
        {
            string pattern = @"(?>((?>13|14)\d\d)|(\d\d))\/(0?[1-9]|1[012])\/([12][0-9]|3[01]|0?[1-9])";
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(persianDateTime, pattern))
                {
                    System.Text.RegularExpressions.Regex d = new System.Text.RegularExpressions.Regex(pattern);
                    int year = int.Parse(d.Match(persianDateTime).Groups[1].Value);
                    int month = int.Parse(d.Match(persianDateTime).Groups[3].Value);
                    int day = int.Parse(d.Match(persianDateTime).Groups[4].Value);


                    throw new NotImplementedException("implement the time");

                    PersianCalendar result = new PersianCalendar();
                    return result.ToDateTime(year, month, day, 0, 0, 0, 0);
                }
            }
            catch (Exception)
            {
                return default(DateTime);
            }
            return default(DateTime);
        }
    }
}
