using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WJOArvioitava.Models.Mapping
{
    public class TuoteMap : EntityTypeConfiguration<Tuote>
    {
        public TuoteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Tietokannan määrityksiä
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.Nimi).IsRequired().HasMaxLength(200);
            this.Property(t => t.Selitys).IsRequired().HasMaxLength(500);
            this.Property(t => t.Hinta).IsRequired().HasPrecision(7, 2);
            this.Property(t => t.VeroId).IsOptional();

            // Taulu ja Sarake -mäppäykset
            this.ToTable("Tuote", "L");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nimi).HasColumnName("Nimi");
            this.Property(t => t.Selitys).HasColumnName("Selitys");
            this.Property(t => t.Hinta).HasColumnName("Hinta");
            this.Property(t => t.VeroId).HasColumnName("VeroId");

            // Viiteavaimien määrityksiä
            this.HasRequired(t => t.Verokanta).WithMany(d => d.Tuotteet).HasForeignKey(t => t.VeroId);

        }
    }
}