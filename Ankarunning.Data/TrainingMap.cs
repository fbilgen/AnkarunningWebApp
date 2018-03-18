using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
    public class TrainingMap
    {
        public TrainingMap(EntityTypeBuilder<Training> entityBuilder) {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Title).IsRequired();
            entityBuilder.Property(e => e.TrainingPlaceId).IsRequired();
            entityBuilder.Property(e => e.DateTime).IsRequired();
            entityBuilder.Property(e => e.Description).IsRequired();

            //nav prop
            entityBuilder.HasOne(e => e.TrainingPhoto).WithOne(e => e.Training);
            entityBuilder.HasOne(e => e.TrainingPlace).WithMany();
        }
    }
}
