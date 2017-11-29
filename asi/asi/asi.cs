using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asi
{
    public class asiDataDefault
    {
        public static Int64 getInt64(object Value)
        {
            Int64 Default = 0;
            return Int64.TryParse(Convert.ToString(Value), out Default) == true ? Convert.ToInt64(Convert.ToString(Value)) : 0;
        }
        public static Int32 getInt32(object Value)
        {
            Int32 Default = 0;
            return Int32.TryParse(Convert.ToString(Value), out Default) == true ? Convert.ToInt32(Convert.ToString(Value)) : 0;
        }
    }
}
