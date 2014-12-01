using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WJOArvioitava.Models.Mapping
{
    public class MaksutapaMap : EntityTypeConfiguration<Maksutapa>
    {
        public MaksutapaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Tietokannan määrityksiä
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Nimi).IsRequired().HasMaxLength(200);

            // Taulu ja Sarake -mäppäykset
            this.ToTable("Maksutapa", "L");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nimi).HasColumnName("Nimi");
        }
    }
}