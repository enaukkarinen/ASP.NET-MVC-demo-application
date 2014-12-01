using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WJOArvioitava.Models.Mapping
{
    public class OstosMap : EntityTypeConfiguration<Ostos>
    {
        public OstosMap()
        {
            // Perusavain
            this.HasKey(t => new {t.Id, t.TuoteId});

            // Tietokannan määrityksiä
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.TuoteId).IsRequired();
            this.Property(t => t.Maara).IsRequired().HasPrecision(9, 2);
            this.Property(t => t.YksikkoHinta).IsRequired().HasPrecision(7, 2);
            this.Property(t => t.AlennusProsentti).IsOptional().HasPrecision(5, 2);

            // Taulu ja sarake -mäppäykset
            this.ToTable("Ostos", "L");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TuoteId).HasColumnName("TuoteId");
            this.Property(t => t.Maara).HasColumnName("Maara");
            this.Property(t => t.YksikkoHinta).HasColumnName("YksikkoHinta");
            this.Property(t => t.AlennusProsentti).HasColumnName("AlennusProsentti");

            // Viiteavaimien määrityksiä
            this.HasRequired(t => t.Ostoskori).WithMany(d => d.Ostokset).HasForeignKey(t => t.Id);
            this.HasRequired(t => t.Tuote).WithMany(d => d.Ostokset).HasForeignKey(t => t.TuoteId);
        }
    }
}