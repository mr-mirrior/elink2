using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Elink
{
    public static class MB
    {
        private static bool OKCancel(string cap, string title, MessageBoxIcon i)
        {
            return DialogResult.OK ==
                MessageBox.Show(cap, title, MessageBoxButtons.OKCancel, i);
        }
        public static bool OKCancelQ(string cap)
        {
            return OKCancel(cap, GetString("s1002", "Confirm"), MessageBoxIcon.Question);
        }
        public static void Info(string cap, string title, MessageBoxIcon i)
        {
            MessageBox.Show(cap, title, MessageBoxButtons.OK, i);
        }
        public static void OKI(string cap)
        {
            Info(cap, "OK", MessageBoxIcon.Information);
        }
        public static bool OKCancelW(string cap, string title)
        {
            return OKCancel(cap, title, MessageBoxIcon.Warning);
        }
        public static void Error(string cap)
        {
            MessageBox.Show(cap, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private static string GetString(string key, string def)
        {
            return Global.GetString(key, def);
        }
        public static void Warning(string cap)
        {
            MessageBox.Show(cap, GetString("s2000", "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private static bool YesNo(string cap, string title, MessageBoxIcon i)
        {
            return DialogResult.Yes ==
                MessageBox.Show(cap, title, MessageBoxButtons.YesNo, i);
        }
        public static DialogResult YesNoCancelQ(string cap)
        {
            return MessageBox.Show(cap, "", MessageBoxButtons.YesNoCancel, 
                MessageBoxIcon.Question);
        }
        public static bool YesNoQ(string cap)
        {
            return YesNo(cap, GetString("s1002", "Confirm"), MessageBoxIcon.Question);
        }
        public static bool YesNoW(string cap, string title)
        {
            return YesNo(cap, title, MessageBoxIcon.Exclamation);
        }
    }
}
