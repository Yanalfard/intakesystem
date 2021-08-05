using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntakeSystem.Utilities
{
    public static class Main
    {
        public static TimeSpan SimplifyTime(TimeSpan span)
        {
            string spanStr = span.ToString();
            string[] spanArr = spanStr.Split(':');
            int[] spanNoArr = new int[3];
            for (int i = 0; i < spanArr.Length; i++)
                spanNoArr[i] = Convert.ToInt32(spanArr[i]);
            if (spanNoArr[1] >= 30)
            {
                if (spanNoArr[1] >= 45)
                {
                    spanNoArr[0]++;
                    spanNoArr[1] = 0;
                }
                else
                    spanNoArr[1] = 30;
            }
            else
            {
                if (spanNoArr[1] >= 15)
                    spanNoArr[1] = 30;
                else
                    spanNoArr[1] = 0;
            }
            return new TimeSpan(spanNoArr[0], spanNoArr[1], 0);
        }
    }
}