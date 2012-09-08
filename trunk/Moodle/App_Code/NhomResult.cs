using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class NhomResult
    {
        long? lngID_Nhom;
        long? lngID_To;
        long lngMaTKB;
        string strTenNhom;
        string strMoTa;
        string strTenTo;

        public long? ID_Nhom
        {
            get { return lngID_Nhom; }
            set { lngID_Nhom = value; }
        }

        public long? ID_To
        {
            get { return lngID_To; }
            set { lngID_To = value; }
        }

        public long MaTKB
        {
            get { return lngMaTKB; }
            set { lngMaTKB = value; }
        }


        public string TenNhom
        {
            get { return strTenNhom; }
            set { strTenNhom = value; }
        }

        public string TenTo
        {
            get { return strTenTo; }
            set { strTenTo = value; }
        }

        public string MoTa
        {
            get { return strMoTa; }
            set { strMoTa = value; }
        }
    }
}