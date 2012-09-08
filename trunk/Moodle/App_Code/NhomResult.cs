using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class NhomResult
    {
        long? lngID_Nhom;
        string strTenNhom;

        public long? ID_Nhom
        {
            get { return lngID_Nhom; }
            set { lngID_Nhom = value; }
        }

        public string TenNhom
        {
            get { return strTenNhom; }
            set { strTenNhom = value; }
        }
    }
}