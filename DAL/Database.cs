using Model;
using System.Collections.Generic;

namespace DAL
{
    public class Database
    {
        public bool TestDBConnection()
        {
            using (var db = new BallonContext())
            {
                var result = db.Database.Exists();

                return result;
            }
        }

        public bool WriteToDb(Ballon ballon)
        {
            using (var db = new BallonContext())
            {
                try
                {
                    db.Balloons.Add(ballon);
                    db.SaveChanges();
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        public IEnumerable<Ballon> ReadFromDb()
        {
            using (var db = new BallonContext())
            {
                foreach (var item in db.Balloons)
                {
                    yield return item;
                }
            }
        }
    }
}
