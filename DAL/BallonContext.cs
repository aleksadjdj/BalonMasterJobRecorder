using Model;
using System.Data.Entity;

namespace DAL
{
    // *** CONFIGURATION FOR DB ***
    //public class BallonConfiguration : EntityTypeConfiguration<Ballon>
    //{
    //    public BallonConfiguration()
    //    {
    //        Property(b => b.Date).HasMaxLength(10);
    //        Property(b => b.Store).IsRequired();
    //    }
    //}

    public class BallonContext : DbContext
    {
        public DbSet<Ballon> Ballon { get; set; }

        // *** CONFIGURATION FOR DB ***
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new BallonConfiguration());
        //}
    }
}
