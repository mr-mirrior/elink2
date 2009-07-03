using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Elink
{
    public class Global
    {
        public static string Language = "es-ES";
        static int langid = 0;
        public static bool IsSpanish()
        {
            return langid == 3082;
        }
        public static void GoMultiLanguage()
        {
            if (Language == null)
                return;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(Language);
            if (ci == null)
                return;

            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            langid = ci.LCID;

            
            //string s = string.Format("{0}", 2.234f);

//             System.Windows.Forms.Application.CurrentCulture = new System.Globalization.CultureInfo(Language);
        }
//         public static string GetString(string key, object o, string def)
//         {
//             return GetString(key, o.GetType(), def);
//         }
        public static string GetString(string key, string def)
        {
            if( langid == 0 )
                return def;
            string s;
            s = Properties.Resources.ResourceManager.GetString(key+"_"+langid.ToString());
//             System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(t);
//             s = resources.GetString(key);
            if (s == null)
                return def;
            if (s.Length == 0)
                return def;
            return s;
        }
    }
}
