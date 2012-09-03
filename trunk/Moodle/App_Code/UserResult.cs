using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class UserResult
    {
        private long? id;
        private string maSV;
        private string ho;
        private string ten;
        private string email;

        public long? Id
        {
            get { return id; }
            set { id = value; }
        }

        public string MaSV
        {
            get { return maSV; }
            set { maSV = value; }
        }

        public string Ho        {
            get { return ho; }
            set { ho = value; }
        }

        public string Ten
        {
            get { return ten; }
            set { ten = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value + "@vimaru.edu.vn"; }
        }
    }
}