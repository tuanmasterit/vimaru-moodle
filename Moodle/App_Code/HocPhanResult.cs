using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class HocPhanResult
    {
        private string maHP;
        private string tenHP;
        public string MaHP
        {
            get { return maHP; }
            set { maHP = value; }
        }

        public string TenHP
        {
            get { return tenHP; }
            set { tenHP = value; }
        }
    }
}