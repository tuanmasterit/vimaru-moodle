using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class ThoiKhoaBieuResult
    {
        long lngId;
        long lngSTT;
        string strTenHP;
        string strMaNH;
        DateTime dtmNgayBD;

        public long Id
        {
            get { return lngId; }
            set { lngId = value; }
        }

        public long STT
        {
            get { return lngSTT; }
            set { lngSTT = value; }
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