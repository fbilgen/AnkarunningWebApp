using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
   public class TrainingPlaceMap
   {
      public TrainingPlaceMap(EntityTypeBuilder<TrainingPlace> entityBuilder)
      {
         entityBuilder.HasKey(e => e.Id);
         entityBuilder.Property(e => e.Name).IsRequired();
      }
    }
}
