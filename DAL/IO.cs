using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace DAL
{
    public class IO
    {
        private readonly string _fileName  = "BalonDataResults.html";
        private readonly string _fullPath;

        public IO()
        {
            _fullPath  = String.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "/", _fileName);
        }

        public bool StartProcess()
        {
            Process proc = new Process
            {
                EnableRaisingEvents = false,
            };
            proc.StartInfo.FileName = _fullPath;

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

        public bool SaveListToHTMLFile(string data)
        {
            using (var file = new StreamWriter(_fullPath, false))
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
