using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;

namespace Logic
{
    public class BallonLogic
    {
        private readonly Database _dataAccess;
        private readonly IO _io;

        public BallonLogic()
        {
            _dataAccess = new Database();
            _io = new IO();
        }

        public bool TestConnection()
        {
            var result = _dataAccess.TestDBConnection();

            return result;
        }

        public bool Write(string date, string store, string dimension, string color, string description)
        {
            var result = _dataAccess.WriteToDb(new Ballon
            {
                Date = date,
                Store = store,
                Dimension = dimension,
                Color = color,
                Description = description,
                QueryInputDate = DateTime.Now
            });

            return result;
        }

        public bool LunchHtmlFile()
        {
            var result = _io.OpenHtmlFile();
            return result;
        }

        public bool CreateHTMLFile()
        {
            IEnumerable<Ballon> ballons = _dataAccess.ReadFromDb();

            var sb = new StringBuilder();

            sb.Append("<!DOCTYPE html>");
            sb.Append("<html><body>");
            sb.Append("<table style=\"width: 100%\" border=\"1\">");
            sb.Append("<tr>");
            sb.Append("<th>Datum posla</th>");
            sb.Append("<th>Radnja</th>");
            sb.Append("<th>Dimenzija</th>");
            sb.Append("<th>Boja</th>");
            sb.Append("<th>Opis</th>");
            sb.Append("<th>Datum unosa</th>");
            sb.Append("</tr>");

            foreach (var b in ballons)
            {
                sb.Append("<tr>");
                sb.Append($"<td>{b.Date}</td>");
                sb.Append($"<td>{b.Store}</td>");
                sb.Append($"<td>{b.Dimension}</td>");
                sb.Append($"<td>{b.Color}</td>");
                sb.Append($"<td>{b.Description}</td>");
                sb.Append($"<td>{b.QueryInputDate}</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table></html></body>");

            var result = _io.SaveTableAsHTMLFile(sb.ToString());

            return result;
        }
    }
}
