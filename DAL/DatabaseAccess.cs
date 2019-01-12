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
                var result = connection.Database.Exists();

                return result;
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

        public IEnumerable<Ballon> Read()
        {
            using (var db = new BallonContext())
            {
                foreach (var item in db.Ballon)
                {
                    yield return item;
                }
            }
        }
    }
}
