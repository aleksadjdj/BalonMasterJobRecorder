using System.Collections.Generic;
using DAL;
using Model;

namespace Logic
{
    public class BalonLogic
    {
        private DatabaseAccess _dataAccess = new DatabaseAccess();

        public bool TestDBConnection()
        {
            var result = _dataAccess.IsServerConnected();

            if (result) return true;
            else return false;

        }

        public void Write(Ballon balon)
        {
            _dataAccess.WriteData(balon);
        }

        public void CreateHTMLFile()
        {
            List<Ballon> ballons = _dataAccess.ReadData();
            new DAL.IO().SaveListToHTMLFile(ballons);
        }
    }
    
}
