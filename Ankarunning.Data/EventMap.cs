using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
    public class EventMap
    {
      public EventMap(EntityTypeBuilder<Event> entityBuilder)
      {
         entityBuilder.HasKey(e => e.Id);
         entityBuilder.Property(e => e.Title).IsRequired();
         entityBuilder.Property(e => e.DateTimeStart).IsRequired();
         entityBuilder.Property(e => e.DateTimeEnd);
         entityBuilder.Property(e => e.Description).IsRequired();
         entityBuilder.Property(e => e.Location).IsRequired();

         //nav prop
         entityBuilder.HasOne(e => e.EventPhoto).WithOne(e => e.Event);

      }
   }
}
