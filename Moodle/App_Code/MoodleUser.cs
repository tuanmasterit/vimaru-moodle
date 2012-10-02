using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace Moodle
{
    public class MoodleUser
    {
        double dblId;
        string strUsername;
        string strPassword;
        string strFirstname;
        string strLastname;
        string strEmail;
        string strTimezone;
        string strCity;
        string strCountry;

        public double Id
        {
            get { return dblId; }
            set { dblId = value; }
        }

        public string Username
        {
            get { return strUsername; }
            set { strUsername = value; }
        }

        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        public string Firstname
        {
            get { return strFirstname; }
            set { strFirstname = value; }
        }

        public string Lastname
        {
            get { return strLastname; }
            set { strLastname = value; }
        }

        public string Email
        {
            get { return strEmail; }
            set { strEmail = value; }
        }

        public string Timezone
        {
            get { return strTimezone; }
            set { strTimezone = value; }
        }

        public string City
        {
            get { return strCity; }
            set { strCity = value; }
        }

        public string Country
        {
            get { return strCountry; }
            set { strCountry = value; }
        }

        public MoodleUser()
        {

        }

        public MoodleUser(string username, string password)
        {
            strUsername = username;
            strPassword = password;
        }

        public MoodleUser(double id, string username, string password)
            : this(username, password)
        {
            dblId = id;
        }

        public MoodleUser(string username, string password, string firstname, string lastname, string email, string timezone, string city, string country)
            : this(username, password)
        {
            strFirstname = firstname;
            strLastname = lastname;
            strEmail = email;
            strTimezone = timezone;
            strCity = city;
            strCountry = country;
        }

        public MoodleUser(double id, string username, string password, string firstname, string lastname, string email, string timezone, string city, string country)
            : this(username, password, firstname, lastname, email, timezone, city, country)
        {
            dblId = id;
        }

        public string GetToken(string service)
        {
            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.LoginUrl, "POST", "username=" + strUsername +                    "&password=" + strPassword + "&service=" + service);

            // Chuỗi có dạng {"token":"d75f2169ec6320a689c67fb5869360e1"}
            string s = web.GetResponse();
            string[] rs = s.Split(new char[]{'"'});
            MoodleUtilites.WriteTextToFile("D:\\token.txt", s);
            // Sau khi tách s[5] = {"","token",":","d75f2169ec6320a689c67fb5869360e1",""}
            if (rs.Count() != 5)
                return "";

            return rs[3].Trim();
        }

        public static string GetToken(string username, string password, string service)
        {
            MoodleUser u = new MoodleUser(username, password);
            return u.GetToken(service);
        }

        public static string CreateUsers(List<MoodleUser> list, string token)
        {
            string postData = "wstoken=" + token + "&wsfunction=core_user_create_users";

            //Duyệt các người dùng trong danh sách lấy thông tin cần cho tạo người dùng
            for (int i = 0; i < list.Count; i++)
            {
                postData += "&users[" + i + "][username]=" + list[i].Username;
                postData += "&users[" + i + "][password]=" + list[i].Password;
                postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(list[i].Firstname);
                postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(list[i].Lastname);
                postData += "&users[" + i + "][email]=" + list[i].Email;
                postData += "&users[" + i + "][timezone]=" + HttpUtility.UrlEncode(list[i].Timezone);
                postData += "&users[" + i + "][city]=" + HttpUtility.UrlEncode(list[i].City);
                postData += "&users[" + i + "][country]=" + HttpUtility.UrlEncode(list[i].Country);
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl,"POST",postData);

            return web.GetResponse();
        }

        public static string UpdateUsers(List<MoodleUser> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_user_update_users";

            //Duyệt các người dùng trong danh sách lấy thông tin cần cập nhật
            for (int i = 0; i < list.Count; i++)
            {
                postData += "&users[" + i + "][id]=" + list[i].Id;
                if(list[i].Username != "" )
                    postData += "&users[" + i + "][username]=" + list[i].Username;

                if (list[i].Password != "")
                    postData += "&users[" + i + "][password]=" + list[i].Password;

                if (list[i].Firstname != "")
                    postData += "&users[" + i + "][firstname]=" + HttpUtility.UrlEncode(list[i].Firstname);

                if (list[i].Lastname != "")
                    postData += "&users[" + i + "][lastname]=" + HttpUtility.UrlEncode(list[i].Lastname);

                if (list[i].Email != "")
                    postData += "&users[" + i + "][email]=" + list[i].Email;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string DeleteUsers(List<int> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_user_delete_users";

            //Duyệt các người dùng trong danh sách lấy id
            for (int i = 0; i < list.Count; i++)
            {
                postData += "&userids[" + i + "]=" + list[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string GetUsersById(List<int> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_user_get_users_by_id";

            //Duyệt các người dùng trong danh sách lấy id
            for (int i = 0; i < list.Count; i++)
            {
                postData += "&userids[" + i + "]=" + list[i];
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }

        public static string GetUsersByCourseId(List<KeyValuePair<int, int> > list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_user_get_course_user_profiles";

            //Duyệt các người dùng trong danh sách lấy id
            for (int i = 0; i < list.Count; i++)
            {
                postData += "&userlist[" + i + "][userid]=" + list[i].Key;
                postData += "&userlist[" + i + "][courseid]=" + list[i].Value;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);

            return web.GetResponse();
        }
    }
}
