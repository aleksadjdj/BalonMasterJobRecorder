using Model;
using System.Data.Entity;

namespace DAL
{
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
        public DbSet<Ballon> Balloons { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new BallonConfiguration());
        //}
    }
}
