using System;
using System.Collections.Generic;
using System.Text;

namespace Lanting.IDCode.Utility
{
    public class RandomHelper
    {
        public static string GenerateRandomCode(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(r.Next(9).ToString());
            }
            return sb.ToString();

        }

        public static string GenerateAntiCode()
        {
            List<string> ranges = new List<string>();
            for (int index = 0; index < 4; index++)
            {
                StringBuilder sb = new StringBuilder();
                Random r = new Random();
                for (int i = 0; i < 4; i++)
                {
                    sb.Append(r.Next(9).ToString());
                }
                ranges.Add(sb.ToString());
            }
            return string.Join(" ", ranges.ToArray());

        }

    }
}
