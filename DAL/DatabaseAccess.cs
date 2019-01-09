using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace DAL
{
    public class DatabaseAccess
    {
        public bool IsServerConnected()
        {
            using (var connection = new BallonContext())
            {
                if (connection.Database.Exists())
                    return true;
                else
                    return false;

            }
        }

        public void WriteData(Ballon ballon)
        {
            using (var db = new BallonContext())
            {
                db.Ballon.Add(ballon);
                db.SaveChanges();
            }
        }

        public List<Ballon> ReadData()
        {
            var ballonList = new List<Ballon>();
            using (var db = new BallonContext())
            {
                foreach (var item in db.Ballon)
                {
                    ballonList.Add(item);
                }
            }
            return ballonList;
        }
    }


    public class BallonContext : DbContext
    {
        public DbSet<Ballon> Ballon { get; set; }

        // *** CONFIGURATION FOR DB ***
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new BallonConfiguration());
        //}
    }

    // *** CONFIGURATION FOR DB ***
    //public class BallonConfiguration : EntityTypeConfiguration<Ballon>
    //{
    //    public BallonConfiguration()
    //    {
    //        Property(b => b.Date).HasMaxLength(10);
    //        Property(b => b.Store).IsRequired();
    //    }
    //}


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
