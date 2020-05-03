using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VBCC.Models;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO.Compression;
using System.IO;

namespace VBCC.Filters
{
    public class UserFunction
    {
        protected static VBCCEntities db = new VBCCEntities();
        public static DateTime? ToDate(string date)
        {
            DateTime? rdate;
            //return rdate;
            string tngay, tthang, tnam;
            if (date == null || date == "")
            {
                tngay = System.DateTime.Now.Day.ToString();
                tthang = System.DateTime.Now.Month.ToString();
                tnam = System.DateTime.Now.Year.ToString();
            }
            else
            {
                tngay = date.Substring(0, 2);
                tthang = date.Substring(3, 2);
                tnam = date.Substring(6, 4);
            }
            if (tngay.Length == 1) { tngay = "0" + tngay; }
            if (tthang.Length == 1) { tthang = "0" + tthang; }
            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            try
            {
                if (sysFormat == "M/d/yyyy")
                {
                    rdate = System.DateTime.Parse(tthang + '/' + tngay + '/' + tnam);

                }
                else
                {
                    rdate = System.DateTime.Parse(date);
                }
                return rdate;
            }
            catch
            {
                DateTime? ndate = null;
                return ndate;
            }

        }        
        public static double? diem(Nullable<double> diem)
        {
            if (diem == null)
            {
                diem = 0;
            }
            return diem;
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}