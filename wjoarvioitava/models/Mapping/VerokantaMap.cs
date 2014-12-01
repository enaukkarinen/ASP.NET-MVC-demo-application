using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WJOArvioitava.Models.Mapping
{
    public class VerokantaMap : EntityTypeConfiguration<Verokanta>
    {
        public VerokantaMap()
        {
            // Perusavain
            this.HasKey(t => t.Id);

            // Tietokannan määrityksiä
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Nimi).IsRequired().HasMaxLength(200);
            this.Property(t => t.Veroprosentti).IsRequired().HasPrecision(5, 2);

            // Taulu ja sarake -mäppäykset
            this.ToTable("Verokanta", "L");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nimi).HasColumnName("Nimi");
            this.Property(t => t.Veroprosentti).HasColumnName("Veroprosentti");
        }
    }
}