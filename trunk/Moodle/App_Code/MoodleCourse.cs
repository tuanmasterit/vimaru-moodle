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
        int intCategoryId;
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

        public int CategoryId
        {
            get { return intCategoryId; }
            set { intCategoryId = value; }
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

        public MoodleCourse(string fullname, string shortname, int categoryid, string idnumber, string summary, int startdate, string forcetheme="", int summaryformat = 1, string format = "weeks", int showgrades = 1, int newsitems = 0, int numsections = 1, int maxbytes = 20971520, int showreports=0, int visible=1, int hiddensections=1, int groupmode=0, int groupmodeforce=0, int defaultgroupingid=0, int enablecompletion=0, int completionstartonenrol=0, int completionnotify=0, string lang="vi")
        {
            strFullName = fullname;
            strShortName = shortname;
            intCategoryId = categoryid;
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

        public static string CreateCourses(List<MoodleCourse> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_course_create_courses";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&courses[" + i + "][fullname]=" + HttpUtility.UrlEncode(list[i].FullName);
                postData += "&courses[" + i + "][shortname]=" + HttpUtility.UrlEncode(list[i].ShortName);
                postData += "&courses[" + i + "][categoryid]=" + list[i].CategoryId;
                postData += "&courses[" + i + "][idnumber]=" + HttpUtility.UrlEncode(list[i].IdNumber);
                postData += "&courses[" + i + "][summary]=" + HttpUtility.UrlEncode(list[i].Summary);
                postData += "&courses[" + i + "][summaryformat]=" + list[i].SummaryFormat;
                postData += "&courses[" + i + "][format]=" + HttpUtility.UrlEncode(list[i].Format);
                postData += "&courses[" + i + "][showgrades]=" + list[i].ShowGrades;
                postData += "&courses[" + i + "][newsitems]=" + list[i].NewsItems;
                postData += "&courses[" + i + "][startdate]=" + list[i].StartDate;
                postData += "&courses[" + i + "][numsections]=" + list[i].NumSections;
                postData += "&courses[" + i + "][maxbytes]=" + list[i].MaxBytes;
                postData += "&courses[" + i + "][showreports]=" + list[i].ShowReports;
                postData += "&courses[" + i + "][visible]=" + list[i].Visible;
                postData += "&courses[" + i + "][hiddensections]=" + list[i].HiddenSections;
                postData += "&courses[" + i + "][groupmode]=" + list[i].GroupMode;
                postData += "&courses[" + i + "][groupmodeforce]=" + list[i].GroupModeForce;
                postData += "&courses[" + i + "][defaultgroupingid]=" + list[i].DefaultGroupingId;
                postData += "&courses[" + i + "][enablecompletion]=" + list[i].EnableCompletion;
                postData += "&courses[" + i + "][completionstartonenrol]=" + list[i].CompletionStartOnEnrol;
                postData += "&courses[" + i + "][completionnotify]=" + list[i].CompletionNotify;
                postData += "&courses[" + i + "][lang]=" + HttpUtility.UrlEncode(list[i].Lang);
                postData += "&courses[" + i + "][forcetheme]=" + HttpUtility.UrlEncode(list[i].ForceTheme);
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string DeleteCourses(List<int> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_course_delete_courses";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&courseids[" + i + "]=" + list[i];
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

        public static string GetCourses(List<int> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_course_get_courses";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&options[ids][" + i + "]=" + list[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }
    }
}