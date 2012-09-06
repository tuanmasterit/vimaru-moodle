using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class HocPhanResult
    {
        long? lngId;
        int intSTT;
        string strMaHP;
        string strTenHP;
        string strMaNH;
        DateTime dtmNgayBD;

        public long? Id
        {
            get { return lngId; }
            set { lngId = value; }
        }

        public int STT
        {
            get { return intSTT; }
            set { intSTT = value; }
        }

        public string MaHP
        {
            get { return strMaHP; }
            set { strMaHP = value; }
        }

        public string TenHP
        {
            get { return strTenHP; }
            set { strTenHP = value; }
        }

        public string MaNH
        {
            get { return strMaNH; }
            set { strMaNH = value; }
        }

        public DateTime NgayBD
        {
            get { return dtmNgayBD; }
            set { dtmNgayBD = value; }
        }
    }
}