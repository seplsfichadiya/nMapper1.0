using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asi
{
    public class asiDataDefault
    {
        public static Int32 getInt32(object Value)
        {
            Int32 Default = 0;
            return Int32.TryParse(Convert.ToString(Value), out Default) == true ? Convert.ToInt32(Convert.ToString(Value)) : 0;
        }
        public static Int32 getInt32(object Value, Int32 Default)
        {
            Int32 outValue = 0;
            return Int32.TryParse(Convert.ToString(Value), out outValue) == true ? Convert.ToInt32(Value) : Default;
        }

        public static Int64 getInt64(object Value)
        {
            Int64 Default = 0;
            return Int64.TryParse(Convert.ToString(Value), out Default) == true ? Convert.ToInt64(Convert.ToString(Value)) : 0;
        }
        public static Int64 getInt64(object Value, Int64 Default)
        {
            Int64 outValue = 0;
            return Int64.TryParse(Convert.ToString(Value), out outValue) == true ? Convert.ToInt64(Value) : Default;
        }

        public static decimal getDecimal(object Value)
        {
            decimal dValue = 0;
            return decimal.TryParse(Convert.ToString(Value), out dValue) == true ? Convert.ToDecimal(Value) : dValue;
        }
        public static decimal getDecimal(object Value, decimal rValue)
        {
            decimal dValue = 0;
            return decimal.TryParse(Convert.ToString(Value), out dValue) == true ? Convert.ToDecimal(Value) : rValue;
        }

        public static DateTime getDateTime(object Value)
        {
            DateTime outValue = new DateTime(1900, 1, 1);
            return DateTime.TryParse(Convert.ToString(Value), out outValue) == true ? Convert.ToDateTime(Value) : outValue;
        }
        public static DateTime getDateTime(object Value, DateTime Default)
        {
            DateTime outValue = new DateTime(1900, 1, 1);
            return DateTime.TryParse(Convert.ToString(Value), out outValue) == true ? Convert.ToDateTime(Value) : Default;
        }

        public static string getString(object Value)
        {
            return Convert.ToString(Value);
        }
    }
}
