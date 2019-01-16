using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace DAL
{
    public class IO
    {
        private readonly string _fileName = "BalloonsRecords.html";

        public bool OpenHtmlFile()
        {
            var proc = new Process();
            proc.StartInfo.FileName = _fileName;

            try
            {
                proc.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveTableAsHTMLFile(string data)
        {
            using (var file = new StreamWriter(_fileName, false))
            {
                try
                {
                    file.WriteLine(data);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
