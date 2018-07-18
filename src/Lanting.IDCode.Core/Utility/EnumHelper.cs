using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Lanting.IDCode.Utility
{
    public class EnumHelper
    {

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="_enum"></param>
        /// <returns></returns>
        public static string Codes(Enum _enum)
        {
            try
            {
                var typeCode = GetEnumDescription(_enum);
                return typeCode;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        private static string GetEnumDescription(Enum enumSubitem)
        {
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return strValue;
            }
            else
            {
                DescriptionAttribute da = (DescriptionAttribute)objs[0];
                return da.Description;
            }

        }
    }
}
