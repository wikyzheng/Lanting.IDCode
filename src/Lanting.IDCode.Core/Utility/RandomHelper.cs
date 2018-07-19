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

    }
}
