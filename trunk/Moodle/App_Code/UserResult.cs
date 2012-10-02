using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class UserResult
    {
        private long lngSTT;
        private long lngId;
        private bool blnGhiDanh;
        private long? lngIdNhom;
        private string strMaSV;
        private string strHo;
        private string strTen;
        private string strEmail;
        private int? intMaLop;
        private string strTenLop;
        private string strTenNhom;

        public long STT
        {
            get { return lngSTT; }
            set { lngSTT = value; }
        }

        public long Id
        {
            get { return lngId; }
            set { lngId = value; }
        }

        public int? MaLop
        {
            get { return intMaLop; }
            set { intMaLop = value; }
        }

        public bool GhiDanh
        {
            get { return blnGhiDanh; }
            set { blnGhiDanh = value; }
        }

        public long? IdNhom
        {
            get { return lngIdNhom; }
            set { lngIdNhom = value; }
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
            set { strEmail = "st" + value + "@st.vimaru.edu.vn"; }
        }

        public string TenLop
        {
            get { return strTenLop; }
            set { strTenLop = value; }
        }

        public string TenNhom
        {
            get { return strTenNhom; }
            set { strTenNhom = value; }
        }
    }
}