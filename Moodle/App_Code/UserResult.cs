using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class UserResult
    {
        private long? lngId;
        private string strMaSV;
        private string strHo;
        private string strTen;
        private string strEmail;

        public long? Id
        {
            get { return lngId; }
            set { lngId = value; }
        }

        public string MaSV
        {
            get { return strMaSV; }
            set { strMaSV = value; }
        }

        public string Ho        {
            get { return strHo; }
            set { strHo = value; }
        }

        public string Ten
        {
            get { return strTen; }
            set { strTen = value; }
        }

        public string Email
        {
            get { return strEmail; }
            set { strEmail = value + "@vimaru.edu.vn"; }
        }
    }
}