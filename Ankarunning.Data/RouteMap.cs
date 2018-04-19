using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ankarunning.Data
{
   public class RouteMap
   {
      public RouteMap(EntityTypeBuilder<Route> entityBuilder)
      {
         entityBuilder.HasKey(e => e.Id);
         entityBuilder.Property(e => e.Name).IsRequired();
         entityBuilder.Property(e => e.Distance).IsRequired();
         entityBuilder.Property(e => e.Latitude).IsRequired();
         entityBuilder.Property(e => e.Longitude).IsRequired();
         entityBuilder.Property(e => e.FileName).IsRequired();
         entityBuilder.Property(e => e.ContentType).IsRequired();
         entityBuilder.Property(e => e.Content).IsRequired();
         entityBuilder.Property(e => e.PhotoFileName).IsRequired();
         entityBuilder.Property(e => e.PhotoContentType).IsRequired();
         entityBuilder.Property(e => e.PhotoContent).IsRequired();
      }
    }
}
