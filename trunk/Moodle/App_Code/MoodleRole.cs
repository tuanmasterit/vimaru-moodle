using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class MoodleRole
    {
        int intRoleId;
        int intUserId;
        int intContextId;

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

        public int ContextId
        {
            get { return intContextId; }
            set { intContextId = value; }
        }

        public MoodleRole() { }

        public MoodleRole(int roleid, int userid, int contextid)
        {
            intRoleId = roleid;
            intUserId = userid;
            intContextId = contextid;
        }

        public static string AssignRoles(List<MoodleRole> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_role_assign_roles";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&assignments[" + i + "][roleid]=" + lst[i].RoleId;
                postData += "&assignments[" + i + "][userid]=" + lst[i].UserId;
                postData += "&assignments[" + i + "][contextid]=" + lst[i].ContextId;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }

        public static string UnassignRoles(List<MoodleRole> lst, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_role_unassign_roles";

            for (int i = 0; i < lst.Count; i++)
            {
                postData += "&unassignments[" + i + "][roleid]=" + lst[i].RoleId;
                postData += "&unassignments[" + i + "][userid]=" + lst[i].UserId;
                postData += "&unassignments[" + i + "][contextid]=" + lst[i].ContextId;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }
    }
}