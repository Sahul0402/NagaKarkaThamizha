using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace KarkaThamizha.Common.Utility
{
    public static class DataConversion
    {
        public static Byte Convert2TinyInt(string data)
        {
            Byte value = 0;
            Byte.TryParse(data, out value);
            return value;
        }

        public static Int16 Convert2Int16(string data)
        {
            Int16 value = 0;
            Int16.TryParse(data.ToString(), NumberStyles.Integer | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value);
            return value;
        }

        public static Int32 Convert2Int32(string data)
        {
            Int32 value = 0;
            Int32.TryParse(data, out value);
            return value;

        }
        public static int Convert2Int(string data)
        {
            int value = -1;
            int.TryParse(data, out value);
            return value;

            //int result = -1;
            //if (!int.TryParse(value, out result))
            //{
            //    result = -1;
            //}
            //return result;
        }

        public static DateTime? ConvertToDate1(string date)
        {
            DateTime? dt = null;
            if (!string.IsNullOrWhiteSpace(date))
            {
                dt = Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo("ta-IN").DateTimeFormat);
            }
            return dt;
        }

        public static DateTime? ConvertToDate2(string text)
        {
            DateTime date = DateTime.MinValue;
            Nullable<DateTime> nDate = null;

            bool isParsed = DateTime.TryParse(text, out date);
            if (isParsed)
                nDate = new Nullable<DateTime>(date);
            else
                nDate = new Nullable<DateTime>();

            return nDate;
        }

        public static DateTime ConvertToDate(string date)
        {
            DateTime result = DateTime.MinValue;

            if (!DateTime.TryParse(date, out result))
            {
                result = DateTime.MinValue;
            }

            return result;
        }

        public static decimal ConvertToDecimal(string value)
        {
            decimal result = 0;
            if (!decimal.TryParse(value, out result))
            {
                result = 0;
            }

            return result;
        }

        public static double ConvertToDouble(string value)
        {
            double result = -1;

            if (!double.TryParse(value, out result))
            {
                result = -1;
            }

            return result;
        }
        public static bool StringToBool(string value)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(value))
            {
                if (value.ToUpper() == "Y" || StringToInt(value) > 0)
                {
                    result = true;
                }
                else
                {
                    bool.TryParse(value, out result);
                }
            }
            return result;
        }
        public static int StringToInt(string value)
        {
            int result = -1;

            if (!int.TryParse(value, out result))
            {
                result = -1;
            }

            return result;
        }
    }
}
