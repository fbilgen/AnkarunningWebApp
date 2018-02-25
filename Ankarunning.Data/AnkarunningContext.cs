using Microsoft.EntityFrameworkCore;

namespace Ankarunning.Data
{
    public class AnkarunningContext : DbContext
    {
        public AnkarunningContext(DbContextOptions<AnkarunningContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);


            #region Training
            new TrainingMap(modelBuilder.Entity<Training>());
            new TrainingPhotoMap(modelBuilder.Entity<TrainingPhoto>());

            #endregion

        }
    }
}
