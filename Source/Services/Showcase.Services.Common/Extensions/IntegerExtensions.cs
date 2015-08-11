namespace Showcase.Services.Common.Extensions
{
    using System;

    public static class IntegerExtensions
    {
        public static string ToMonthName(this int monthIndex)
        {
            switch (monthIndex)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default:
                    throw new ArgumentException("Not a valid month index");
            }
        }

        public static string ToUrlPath(this int id)
        {
            return string.Format("{0}/{1}", id % Constants.SavedFilesSubfoldersCount, string.Format("{0}{1}", id.ToMd5Hash().Substring(0, Constants.FileHashLength), id));
        }
    }
}
