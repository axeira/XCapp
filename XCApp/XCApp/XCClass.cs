using System;
using System.Collections.Generic;
using System.Text;

namespace XCApp
{
    class XCClass
    {


        public static Boolean IsErrorLabelVisible { get; set; }

        public static string SecondsToString(double seconds, Boolean ShowMilliseconds = false)
        {
            string r = "";
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            if (t.Hours != 0) r = t.Hours.ToString("00") + ":";
            r = r + t.Minutes.ToString("00") + ":";
            r = r + t.Seconds.ToString("00");
            if (ShowMilliseconds) r = r + "." + (t.Milliseconds / 100).ToString("0");

            return r;
        }



    }
}
