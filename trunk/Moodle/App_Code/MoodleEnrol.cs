using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class MoodleEnrol
    {
        int intRoleId;
        int intUserId;
        int intCourseId;
        int intTimeStart;
        int intTimeEnd;
        int intSuspend;

        public int RoleId
        {
            get { return intRoleId; }
            set { intRoleId = value; }
        }

        public int UserId
        {
            get { return intUserId; }
            set { intUserId = value; }
        }

        public int CourseId
        {
            get { return intCourseId; }
            set { intCourseId = value; }
        }

        public int TimeStart
        {
            get { return intTimeStart; }
            set { intTimeStart = value; }
        }

        public int TimeEnd
        {
            get { return intTimeEnd; }
            set { intTimeEnd = value; }
        }

        public int Suspend
        {
            get { return intSuspend; }
            set { intSuspend = value; }
        }

        public MoodleEnrol() { }

        public MoodleEnrol(int roleid, int userid, int courseid, int timestart, int timeend, int suspend)
        {
            intRoleId = roleid;
            intUserId = userid;
            intCourseId = courseid;
            intTimeStart = timestart;
            intTimeEnd = timeend;
            intSuspend = suspend;
        }

        public static string EnrolUsers(List<MoodleEnrol> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=enrol_manual_enrol_users";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&enrolments[" + i + "][roleid]=" + lst[i].RoleId;
                postData += "&enrolments[" + i + "][userid]=" + lst[i].UserId;
                postData += "&enrolments[" + i + "][courseid]=" + lst[i].CourseId;
                postData += "&enrolments[" + i + "][timestart]=" + lst[i].TimeStart;
                postData += "&enrolments[" + i + "][timeend]=" + lst[i].TimeEnd;
                postData += "&enrolments[" + i + "][suspend]=" + lst[i].Suspend;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            

            
            return web.GetResponse();
        }

        public static string GetEnrolledUsers(int courseId, List<KeyValuePair<string, string>> options, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_enrol_get_enrolled_users";
            postData += "&courseid=" + courseId;

            for (int i = 0; i < options.Count; i++)
            {
                postData += "&options[" + i + "][name]=" + options[i].Key;
                postData += "&options[" + i + "][value]=" + options[i].Value;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string GetUsersCourses(int userId, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_enrol_get_users_courses";
            postData += "&userid=" + userId;
            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }
    }
}