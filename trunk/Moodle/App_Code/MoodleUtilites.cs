using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Moodle
{
    public class MoodleUtilites
    {
        /// <summary>
        /// method for get short name of full name
        /// </summary>
        /// <param name="fullName">full name to get short name</param>
        /// <returns></returns>
        public static string GetShortName(string fullName)
        {
            string rs = "";
            string[] split = fullName.Split(new char[]{' '});
            int len = split.Count()-1;
            for (int i = 0; i < len; ++i)
            {
                rs += split[i].Substring(0, 1).ToLower();
            }
            rs += "-" + split[len];
            return rs;
        }
        /// <summary>
        /// method for converting a System.DateTime value to a UNIX Timestamp
        /// </summary>
        /// <param name="value">date to convert</param>
        /// <returns>Timestamp</returns>
        public static int ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (int)span.TotalSeconds;
        }
        /// <summary>
        /// method for converting a Date Time String to a UNIX Timestamp
        /// </summary>
        /// <param name="dateString">date string to convert</param>
        /// <returns>System.DateTime</returns>
        public static int ConvertToTimestamp(string dateString)
        {
            //get a System.DateTime from DateTime String
            DateTime value = ConvertToDate(dateString);
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (int)span.TotalSeconds;
        }
        /// <summary>
        /// method for converting a UNIX timestamp to a regular
        /// System.DateTime value (and also to the current local time)
        /// </summary>
        /// <param name="timestamp">value to be converted</param>
        /// <returns>converted DateTime in string format</returns>
        public static DateTime ConvertTimestamp(int timestamp)
        {
            //create a new DateTime value based on the Unix Epoch
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            //add the timestamp to the value
            DateTime newDateTime = converted.AddSeconds(timestamp);

            //return the value in string format
            return newDateTime.ToLocalTime();
        }
        /// <summary>
        /// method for converting a Date Time String in cultureInfo("fr-FR")
        ///  to a regular System.DateTime value (and also to the current local time)
        /// </summary>
        /// <param name="dateString">date string to be converted</param>
        /// <returns>converted DateTime in string format</returns>
        public static DateTime ConvertToDate(string dateString)
        {
            //get string array from date string in format "dd-MM-yyyy" 
            string[] s = dateString.Split(new char[] { '-' });

            //return the value in string format
            return new DateTime(Convert.ToInt32(s[2]), Convert.ToInt32(s[1]),Convert.ToInt32(s[0])).ToLocalTime();
        }
    }
}