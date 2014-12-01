using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WJOArvioitava.Models.Mapping
{
    public class AsiakasMap : EntityTypeConfiguration<Asiakas>
    {
        public AsiakasMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Tietokannan määrityksiä
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Nimi).IsRequired().HasMaxLength(265);
            this.Property(t => t.Sahkoposti).IsOptional().HasMaxLength(200);
            this.Property(t => t.Katuosoite).IsOptional().HasMaxLength(200);
            this.Property(t => t.Postinro).IsOptional().IsFixedLength().HasMaxLength(5);
            this.Property(t => t.KaupunkiId).IsOptional();
            this.Property(t => t.AlennusProsentti).IsOptional().HasPrecision(5, 2);
            this.Property(t => t.Maksukortti).IsOptional().HasMaxLength(200);

            // Taulu ja Sarake -mäppäykset
            this.ToTable("Asiakas", "L");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nimi).HasColumnName("Nimi");
            this.Property(t => t.Sahkoposti).HasColumnName("Sahkoposti");
            this.Property(t => t.Katuosoite).HasColumnName("Katuosoite");
            this.Property(t => t.Postinro).HasColumnName("Postinro");
            this.Property(t => t.KaupunkiId).HasColumnName("Kaupunki");
            this.Property(t => t.AlennusProsentti).HasColumnName("AlennusProsentti");
            this.Property(t => t.Maksukortti).HasColumnName("Maksukortti");

            // Viiteavaimien määrityksiä
            this.HasOptional(t => t.Kaupunki).WithMany(t => t.Asiakkaat).HasForeignKey(d => d.KaupunkiId);
        }
    }
}