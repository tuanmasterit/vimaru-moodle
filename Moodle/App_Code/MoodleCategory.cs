using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class MoodleCategory
    {
        int intId;
        string strName;
        int intParent;
        string strIdnumber;
        string strDescription;
        int intDescriptionformat;
        string strTheme;

        public int Id
        {
            get { return intId; }
            set { intId = value; }
        }

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        public int Parent
        {
            get { return intParent; }
            set { intParent = value; }
        }

        public string IdNumber
        {
            get { return strIdnumber; }
            set { strIdnumber = value; }
        }

        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }

        public int DescriptionFormat
        {
            get { return intDescriptionformat; }
            set { intDescriptionformat = value; }
        }

        public string Theme
        {
            get { return strTheme; }
            set { strTheme = value; }
        }

        public MoodleCategory()
        {

        }

        public MoodleCategory(string name, string idnumber, string description, int parent = 0, int descriptionformat = 1, string theme = null)
        {
            strName = name;
            intParent = parent;
            strIdnumber = idnumber;
            strDescription = description;
            intDescriptionformat = descriptionformat;
            strTheme = theme;
        }

        public MoodleCategory(int id, string name, string idnumber, string description, int parent = 0, int descriptionformat = 1, string theme = null)
            : this(name, idnumber, description, parent, descriptionformat, theme)
        {
            intId = id;
        }

        public static string CreateCategories(List<MoodleCategory> lstCategory, string token)
        {
            string postData = "wstoken=" + token + "&wsfunction=core_course_create_categories";

            //Duyệt các Category trong danh sách lấy thông tin cần cho tạo mục khóa học
            for (int i = 0; i < lstCategory.Count; i++)
            {
                postData += "&categories[" + i + "][name]=" + HttpUtility.UrlEncode(lstCategory[i].Name);
                postData += "&categories[" + i + "][parent]=" + lstCategory[i].Parent;
                postData += "&categories[" + i + "][idnumber]=" + HttpUtility.UrlEncode(lstCategory[i].IdNumber);
                postData += "&categories[" + i + "][description]=" + HttpUtility.UrlEncode(lstCategory[i].Description);
                postData += "&categories[" + i + "][descriptionformat]=" + lstCategory[i].DescriptionFormat;
                if (lstCategory[i].Theme != null) postData += "&categories[" + i + "][theme]=" + lstCategory[i].Theme;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl, "POST", postData);

            return web.GetResponse();
        }

        public static string UpdateCategories(List<MoodleCategory> lstCategory, string token)
        {
            string postData = "wstoken=" + token + "&wsfunction=core_course_update_categories";

            //Duyệt các Category trong danh sách lấy thông tin cập nhật
            for (int i = 0; i < lstCategory.Count; i++)
            {
                postData += "&categories[" + i + "][id]=" + lstCategory[i].Id;
                postData += "&categories[" + i + "][name]=" + HttpUtility.UrlEncode(lstCategory[i].Name);
                postData += "&categories[" + i + "][parent]=" + lstCategory[i].Parent;
                postData += "&categories[" + i + "][idnumber]=" + HttpUtility.UrlEncode(lstCategory[i].IdNumber);
                postData += "&categories[" + i + "][description]=" + HttpUtility.UrlEncode(lstCategory[i].Description);
                postData += "&categories[" + i + "][descriptionformat]=" + lstCategory[i].DescriptionFormat;
                postData += "&categories[" + i + "][theme]=" + lstCategory[i].Theme;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl, "POST", postData);

            return web.GetResponse();
        }
    }

    public class MoodleDeleteCategory
    {
        int intId;
        int intNewParent;
        int intRecursive;

        public int Id
        {
            get { return intId; }
            set { intId = value; }
        }

        public int NewParent
        {
            get { return intNewParent; }
            set { intNewParent = value; }
        }

        public int Recursive
        {
            get { return intRecursive; }
            set { intRecursive = value; }
        }

        public MoodleDeleteCategory()
        {

        }

        public MoodleDeleteCategory(int id, int newparent, int recursive)
        {
            intId = id;
            intNewParent = newparent;
            intRecursive = recursive;
        }

        public static string DeleteCategories(List<MoodleDeleteCategory> lstDeleteCategory, string token)
        {
            string postData = "wstoken=" + token + "&wsfunction=core_course_delete_categories";

            //Duyệt các Category trong danh sách lấy thông tin cập nhật
            for (int i = 0; i < lstDeleteCategory.Count; i++)
            {
                postData += "&categories[" + i + "][id]=" + lstDeleteCategory[i].Id;
                postData += "&categories[" + i + "][newparent]=" + lstDeleteCategory[i].NewParent;
                postData += "&categories[" + i + "][recursive]=" + lstDeleteCategory[i].Recursive;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl, "POST", postData);

            return web.GetResponse();
        }
    }

    public class MoodleGetCategory
    {
        KeyValuePair<string, string> criteria;

        public KeyValuePair<string, string> Criteria
        {
            get { return criteria; }
            set { criteria = value; }
        }
           

        public MoodleGetCategory()
        {

        }

        public MoodleGetCategory(KeyValuePair<string, string> keyvalue)
        {
            criteria = keyvalue;
        }
       
        public MoodleGetCategory(string key, string value)
        {
            criteria = new KeyValuePair<string, string>(key, value);
        }
         
        public static string GetCategories(List<MoodleGetCategory> lstGetCategory, bool addSubCategories, string token)
        {
            string postData = "wstoken=" + token + "&wsfunction=core_course_get_categories";

            //Duyệt các Category trong danh sách lấy thông tin cập nhật
            for (int i = 0; i < lstGetCategory.Count; i++)
            {
                postData += "&criteria[" + i + "][key]=" + lstGetCategory[i].Criteria.Key;
                postData += "&criteria[" + i + "][value]=" + HttpUtility.UrlEncode(lstGetCategory[i].Criteria.Value);
            }

            if (addSubCategories==false)
                postData += "&addsubcategories=0";
            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl, "POST", postData);

            return web.GetResponse();
        }
    }
}