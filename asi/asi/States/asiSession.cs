using System;
using System.Web;
namespace asi.States
{
    class asiSession
    {
        public static void setSession(string Name, object Value)
        {
            HttpContext.Current.Session[Name] = Value;
        }
        public static object getSession(string Name)
        {
            return HttpContext.Current.Session[Name];
        }
        public static bool IsSession(string Name)
        {
            return HttpContext.Current.Session[Name] != null;
        }
        public static void UnsetSession(string Name)
        {
            HttpContext.Current.Session.Remove(Name);
        }
        public static void UnsetSession()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
