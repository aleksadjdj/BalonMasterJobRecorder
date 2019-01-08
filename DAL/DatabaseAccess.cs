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
        public void StartProcess(string filePath)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process
            {
                EnableRaisingEvents = false,
            };
            proc.StartInfo.FileName = filePath;

            try
            {
                proc.Start();
            }
            catch (Exception ex)
            {
                Trace.Write(ex);
            }
        }

        public void SaveListToHTMLFile(List<Ballon> ballons)
        {
            string fileName = Guid.NewGuid() + "_dbData.html";
            string fullPath = String.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "/", fileName);

            using (var file = new StreamWriter(fullPath, true))
            {
                file.WriteLine("<!DOCTYPE html>");
                file.WriteLine("<html><body>");
                file.WriteLine("<table style=\"width: 100%\" border=\"1\">");
                file.WriteLine("<tr>");
                file.WriteLine("<th>Datum</th>");
                file.WriteLine("<th>Radnja</th>");
                file.WriteLine("<th>Dimenzija</th>");
                file.WriteLine("<th>Boja</th>");
                file.WriteLine("<th>Opis</th>");
                file.WriteLine("<th>Datum unosa</th>");
                file.WriteLine("</tr>");

                foreach (var b in ballons)
                {
                    file.WriteLine("<tr>");
                    file.WriteLine($"<td>{b.Date}</td>");
                    file.WriteLine($"<td>{b.Store}</td>");
                    file.WriteLine($"<td>{b.Dimension}</td>");
                    file.WriteLine($"<td>{b.Color}</td>");
                    file.WriteLine($"<td>{b.Description}</td>");
                    file.WriteLine($"<td>{b.QueryInputDate}</td>");
                    file.WriteLine("</tr>");
                }
                file.WriteLine("</table></html></body>");
            }

            StartProcess(fullPath);
        }
    }
}
