using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class MoodleCourse
    {
        int intId;
        string strFullName;
        string strShortName;
        int intCategory;
        string strIdNumber;
        string strSummary;
        int intSummaryFormat;
        string strFormat;
        int intShowGrades;
        int intNewsItems;
        int intStartDate;
        int intNumSections;
        int intMaxBytes;
        int intShowReports;
        int intVisible;
        int intHiddenSections;
        int intGroupMode;
        int intGroupModeForce;
        int intDefaultGroupingId;
        int intEnableCompletion;
        int intCompletionStartOnEnrol;
        int intCompletionNotify;
        string strLang;
        string strForceTheme;

        public int Id
        {
            get { return intId; }
            set { intId = value; }
        }

        public string FullName
        {
            get { return strFullName; }
            set { strFullName = value; }
        }

        public string ShortName
        {
            get { return strShortName; }
            set { strShortName = value; }
        }

        public int Category
        {
            get { return intCategory; }
            set { intCategory = value; }
        }

        public string IdNumber
        {
            get { return strIdNumber; }
            set { strIdNumber = value; }
        }

        public string Summary
        {
            get { return strSummary; }
            set { strSummary = value; }
        }

        public int SummaryFormat
        {
            get { return intSummaryFormat; }
            set { intSummaryFormat = value; }
        }

        public string Format
        {
            get { return strFormat; }
            set { strFormat = value; }
        }

        public int ShowGrades
        {
            get { return intShowGrades; }
            set { intShowGrades = value; }
        }

        public int NewsItems
        {
            get { return intNewsItems; }
            set { intNewsItems = value; }
        }

        public int StartDate
        {
            get { return intStartDate; }
            set { intStartDate = value; }
        }

        public int NumSections
        {
            get { return intNumSections; }
            set { intNumSections = value; }
        }

        public int MaxBytes
        {
            get { return intMaxBytes; }
            set { intMaxBytes = value; }
        }

        public int ShowReports
        {
            get { return intShowReports; }
            set { intShowReports = value; }
        }

        public int Visible
        {
            get { return intVisible; }
            set { intVisible = value; }
        }

        public int HiddenSections
        {
            get { return intHiddenSections; }
            set { intHiddenSections = value; }
        }

        public int GroupMode
        {
            get { return intGroupMode; }
            set { intGroupMode = value; }
        }

        public int GroupModeForce
        {
            get { return intGroupModeForce; }
            set { intGroupModeForce = value; }
        }

        public int DefaultGroupingId
        {
            get { return intDefaultGroupingId; }
            set { intDefaultGroupingId = value; }
        }

        public int EnableCompletion
        {
            get { return intEnableCompletion; }
            set { intEnableCompletion = value; }
        }

        public int CompletionStartOnEnrol
        {
            get { return intCompletionStartOnEnrol; }
            set { intCompletionStartOnEnrol = value; }
        }

        public int CompletionNotify
        {
            get { return intCompletionNotify; }
            set { intCompletionNotify = value; }
        }

        public string Lang
        {
            get { return strLang; }
            set { strLang = value; }
        }

        public string ForceTheme
        {
            get { return strForceTheme; }
            set { strForceTheme = value; }
        }

        public MoodleCourse() { }

        public MoodleCourse(string fullname, string shortname, int category, string idnumber, string summary, int summaryformat, string format, int showgrades, int newsitems, int startdate, int numsections, int maxbytes, int showreports, int visible, int hiddensections, int groupmode, int groupmodeforce, int defaultgroupingid, int enablecompletion, int completionstartonenrol, int completionnotify, string lang, string forcetheme)
        {
            strFullName = fullname;
            strShortName = shortname;
            intCategory = category;
            strIdNumber = idnumber;
            strSummary = summary;
            intSummaryFormat = summaryformat;
            strFormat = format;
            intShowGrades = showgrades;
            intNewsItems = newsitems;
            intStartDate = startdate;
            intNumSections = numsections;
            intMaxBytes = maxbytes;
            intShowReports = showreports;
            intVisible = visible;
            intHiddenSections = hiddensections;
            intGroupMode = groupmode;
            intGroupModeForce = groupmodeforce;
            intDefaultGroupingId = defaultgroupingid;
            intEnableCompletion = enablecompletion;
            intCompletionStartOnEnrol = completionstartonenrol;
            intCompletionNotify = completionnotify;
            strLang = lang;
            strForceTheme = forcetheme;
        }

        public static string CreateCourses(List<MoodleCourse> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_course_create_courses";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&courses[" + i + "][fullname]=" + HttpUtility.UrlEncode(lst[i].FullName);
                postData += "&courses[" + i + "][shortname]=" + HttpUtility.UrlEncode(lst[i].ShortName);
                postData += "&courses[" + i + "][category]=" + lst[i].Category;
                postData += "&courses[" + i + "][idnumber]=" + HttpUtility.UrlEncode(lst[i].IdNumber);
                postData += "&courses[" + i + "][summary]=" + HttpUtility.UrlEncode(lst[i].Summary);
                postData += "&courses[" + i + "][summaryformat]=" + lst[i].SummaryFormat;
                postData += "&courses[" + i + "][format]=" + HttpUtility.UrlEncode(lst[i].Format);
                postData += "&courses[" + i + "][showgrades]=" + lst[i].ShowGrades;
                postData += "&courses[" + i + "][newsitems]=" + lst[i].NewsItems;
                postData += "&courses[" + i + "][startdate]=" + lst[i].StartDate;
                postData += "&courses[" + i + "][numsections]=" + lst[i].NumSections;
                postData += "&courses[" + i + "][maxbytes]=" + lst[i].MaxBytes;
                postData += "&courses[" + i + "][showreports]=" + lst[i].ShowReports;
                postData += "&courses[" + i + "][visible]=" + lst[i].Visible;
                postData += "&courses[" + i + "][hiddensections]=" + lst[i].HiddenSections;
                postData += "&courses[" + i + "][groupmode]=" + lst[i].GroupMode;
                postData += "&courses[" + i + "][groupmodeforce]=" + lst[i].GroupModeForce;
                postData += "&courses[" + i + "][defaultgroupingid]=" + lst[i].DefaultGroupingId;
                postData += "&courses[" + i + "][enablecompletion]=" + lst[i].EnableCompletion;
                postData += "&courses[" + i + "][completionstartonenrol]=" + lst[i].CompletionStartOnEnrol;
                postData += "&courses[" + i + "][completionnotify]=" + lst[i].CompletionNotify;
                postData += "&courses[" + i + "][lang]=" + HttpUtility.UrlEncode(lst[i].Lang);
                postData += "&courses[" + i + "][forcetheme]=" + HttpUtility.UrlEncode(lst[i].ForceTheme);
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string DeleteCourses(List<int> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_course_delete_courses";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&courseids[" + i + "]=" + lst[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string GetCourseContents(int courseId, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_course_get_contents";
            postData += "&courseid=" + courseId;

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string GetCourses(List<int> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_course_get_courses";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&options[ids][" + i + "]=" + lst[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }
    }
}