using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WJOArvioitava.Models.Mapping;

namespace WJOArvioitava.Models
{
    public partial class KauppaContext : DbContext
    {
        // Konstruktorit
        public KauppaContext()
            : base("WJO")
        {

        }
        static KauppaContext()
        {
            Database.SetInitializer<KauppaContext>(null);
        }

        // Ominaisuudet
        public DbSet<Asiakas> Asiakkaat { get; set; }
        public DbSet<Kaupunki> Kaupungit { get; set; }
        public DbSet<Maksutapa> Maksutavat { get; set; }
        public DbSet<Ostoskori> Ostoskorit { get; set; }
        public DbSet<OstoskoriTila> OstoskoriTilat { get; set; }
        public DbSet<Ostos> Ostokset { get; set; }
        public DbSet<Tuote> Tuotteet { get; set; }
        public DbSet<Verokanta> Verokannat { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AsiakasMap());
            modelBuilder.Configurations.Add(new KaupunkiMap());
            modelBuilder.Configurations.Add(new MaksutapaMap());
            modelBuilder.Configurations.Add(new OstoskoriMap());
            modelBuilder.Configurations.Add(new OstoskoriTilaMap());
            modelBuilder.Configurations.Add(new OstosMap());
            modelBuilder.Configurations.Add(new TuoteMap());
            modelBuilder.Configurations.Add(new VerokantaMap());
        }
    }
}