using Model;
using System.Collections.Generic;

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

        public bool Write(Ballon ballon)
        {
            using (var db = new BallonContext())
            {
                try
                {
                    db.Ballon.Add(ballon);
                    db.SaveChanges();
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        public List<Ballon> Read()
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
}
