using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WJOArvioitava.Models.Mapping
{
    public class OstoskoriMap : EntityTypeConfiguration<Ostoskori>
    {
        public OstoskoriMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Tietokannan määrityksiä
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ViimeksiMuokattu).IsRequired();
            this.Property(t => t.TilausPvm).IsOptional();
            this.Property(t => t.Tilaaja).IsRequired();
            this.Property(t => t.Tila).IsRequired();
            this.Property(t => t.MaksutapaId).IsRequired();

            // Taulu ja sarake -mäppäykset
            this.ToTable("Ostoskori", "L");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ViimeksiMuokattu).HasColumnName("ViimeksiMuokattu");
            this.Property(t => t.TilausPvm).HasColumnName("TilausPvm");
            this.Property(t => t.Tilaaja).HasColumnName("Tilaaja");
            this.Property(t => t.Tila).HasColumnName("Tila");
            this.Property(t => t.MaksutapaId).HasColumnName("Maksutapa");

            // Viiteavaimien määrityksiä
            this.HasRequired(t => t.Maksutapa).WithMany(d => d.Ostoskorit).HasForeignKey(t => t.MaksutapaId);
            this.HasRequired(t => t.OstoskoriTila).WithMany(d => d.Ostoskorit).HasForeignKey(t => t.Tila);
            this.HasRequired(t => t.Asiakas).WithMany(d => d.Ostoskorit).HasForeignKey(t => t.Tilaaja);
        }
    }
}