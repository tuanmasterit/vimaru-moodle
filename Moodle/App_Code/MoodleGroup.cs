using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class MoodleGroup
    {
        int intId;
        int intCourseId;
        string strName;
        string strDescription;
        int intDescriptionFormat;
        string strEnrolmentKey;

        public int Id
        {
            get { return intId; }
            set { intId = value; }
        }

        public int CourseId
        {
            get { return intCourseId; }
            set { intCourseId = value; }
        }

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }

        public int DescriptionFormat
        {
            get { return intDescriptionFormat; }
            set { intDescriptionFormat = value; }
        }

        public string EnrolmentKey
        {
            get { return strEnrolmentKey; }
            set { strEnrolmentKey = value; }
        }

        public MoodleGroup() { }

        public MoodleGroup(int courseid, string name, string description, int descriptionformat, string enrolmentkey)
        {
            intCourseId = courseid;
            strName = name;
            strDescription = description;
            intDescriptionFormat = descriptionformat;
            strEnrolmentKey = enrolmentkey;
        }

        public static string CreateGroups(List<MoodleGroup> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_create_groups";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&groups[" + i + "][courseid]=" + list[i].CourseId;
                postData += "&groups[" + i + "][name]=" + HttpUtility.UrlEncode(list[i].Name);
                postData += "&groups[" + i + "][description]=" + HttpUtility.UrlEncode(list[i].Description);
                postData += "&groups[" + i + "][descriptionformat]=" + list[i].DescriptionFormat;
                postData += "&groups[" + i + "][enrolmentkey]=" + HttpUtility.UrlEncode(list[i].EnrolmentKey);
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string CreateGroupings(List<MoodleGroup> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_create_groupings";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&groupings[" + i + "][courseid]=" + list[i].CourseId;
                postData += "&groupings[" + i + "][name]=" + HttpUtility.UrlEncode(list[i].Name);
                postData += "&groupings[" + i + "][description]=" + HttpUtility.UrlEncode(list[i].Description);
                postData += "&groupings[" + i + "][descriptionformat]=" + list[i].DescriptionFormat;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string UpdateGroupings(List<MoodleGroup> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_update_groupings";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&groupings[" + i + "][id]=" + list[i].Id;
                postData += "&groupings[" + i + "][name]=" + HttpUtility.UrlEncode(list[i].Name);
                postData += "&groupings[" + i + "][description]=" + HttpUtility.UrlEncode(list[i].Description);
                postData += "&groupings[" + i + "][descriptionformat]=" + list[i].DescriptionFormat;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string AddGroupMembers(List<KeyValuePair<int, int>> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_add_group_members";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&members[" + i + "][groupid]=" + list[i].Key;
                postData += "&members[" + i + "][userid]=" + list[i].Value;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string DeleteGroupMembers(List<KeyValuePair<int, int>> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_delete_group_members";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&members[" + i + "][groupid]=" + list[i].Key;
                postData += "&members[" + i + "][userid]=" + list[i].Value;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string DeleteGroups(List<int> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_delete_groups";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&groupids[" + i + "]=" + list[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string DeleteGroupings(List<int> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_delete_groupings";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&groupingids[" + i + "]=" + list[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string AssignGrouping(List<KeyValuePair<int, int>> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_assign_grouping";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&assignments[" + i + "][groupingid]=" + list[i].Key;
                postData += "&assignments[" + i + "][groupid]=" + list[i].Value;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string UnassignGrouping(List<KeyValuePair<int, int>> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_unassign_grouping";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&unassignments[" + i + "][groupingid]=" + list[i].Key;
                postData += "&unassignments[" + i + "][groupid]=" + list[i].Value;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }
        public static string GetCourseGroupings(int courseId, string token)
        {

            string postData = "?wstoken=" + token + "&wsfunction=core_group_get_course_groupings";
            postData += "&courseid=" + courseId;

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string GetCourseGroups(int courseId, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_get_course_groupis";
            postData += "&courseid=" + courseId;

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string GetGroupMembers(List<int> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_get_group_members";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&groupids[" + i + "]=" + lst[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string GetGroupings(List<int> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_get_groupings";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&groupingids[" + i + "]=" + lst[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string GetGroups(List<int> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_group_get_groups";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&groupids[" + i + "]=" + lst[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }
    }
}