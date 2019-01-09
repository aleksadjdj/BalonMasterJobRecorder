using System.Collections.Generic;
using System.Text;
using DAL;
using Model;

namespace Logic
{
    public class BalonLogic
    {
        private DatabaseAccess _dataAccess = new DatabaseAccess();
        private IO _io = new IO();

        public bool TestDBConnection()
        {
            var result = _dataAccess.IsServerConnected();

            if (result)
                return true;
            else return 
                    false;

        }

        public void Write(Ballon balon)
        {
            _dataAccess.WriteData(balon);
        }

        public bool LunchHtmlFile()
        {
            var result = _io.StartProcess();
            return result;
        }

        public bool CreateHTMLFile()
        {
            List<Ballon> ballons = _dataAccess.ReadData();
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

            var result = _io.SaveListToHTMLFile(sb.ToString());
            return result;
        }
    }
    
}
