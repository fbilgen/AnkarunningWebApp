using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
    public class TrainingPhotoMap
    {
        public TrainingPhotoMap(EntityTypeBuilder<TrainingPhoto> entityBuilder) {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Name);
            entityBuilder.Property(e => e.ContentType);
            entityBuilder.Property(e => e.Content);

            //nav prop
            entityBuilder.HasOne(e => e.Training).WithOne(e => e.TrainingPhoto);
        }
    }
}
