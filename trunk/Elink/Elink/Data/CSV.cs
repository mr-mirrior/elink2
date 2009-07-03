using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
 
namespace Elink
{
    public class CSV
    {
        public List<string[]> lines = new List<string[]>();
        //int cols = 0;
        //int rows = 0;
        public CSV()
        {

        }
        public void Load(string file)
        {
            List<float> hourly = new List<float>();
            List<float> daily = new List<float>();
            List<float> weekly = new List<float>();
            List<float> monthly = new List<float>();
            List<float> max_monthly = new List<float>();

            ReadLines(file);
            foreach (string[] line in lines)
            {

            }
        }
        public void Save(string file)
        {
            StreamWriter txt = null;
            try
            {
                txt = new StreamWriter(file);
                foreach (string[] line in lines)
                {
                    txt.WriteLine(string.Join(",", line));
                }

            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
            }
            finally
            {
                if (txt != null)
                    txt.Close();
            }
        }
        public void Parse()
        {
            //             if( lines.Count == 0 )
            //                 return;
            //             rows = lines.Count;
            //             string[] strs = lines[0].Split(',');
            //             cols = strs.Length;
        }
        public bool ReadLines(string file)
        {
            lines.Clear();
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(file, FileMode.Open);
                sr = new StreamReader(fs);
                string line = sr.ReadLine();
                while (line != null)
                {
                    lines.Add(line.Split(','));
                    line = sr.ReadLine();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
                if (fs != null)
                    fs.Close();
            }
            return true;

        }
    }
}
