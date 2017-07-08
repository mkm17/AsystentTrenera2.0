using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AsystentTrenera2.Models
{
    public class AsystentTrenera2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<AsystentTrenera2.Models.AsystentTrenera2Context>());

        public DbSet<AsystentTrenera2.Models.Ocena> Ocena { get; set; }

        public DbSet<AsystentTrenera2.Models.Zawodnik> Zawodnik { get; set; }

        public DbSet<AsystentTrenera2.Models.Mecz> Mecz { get; set; }

        public DbSet<AsystentTrenera2.Models.Pozycja> Pozycja { get; set; }

        public DbSet<AsystentTrenera2.Models.OcenaOgolna> OcenaOgolna { get; set; }

        public DbSet<AsystentTrenera2.Models.Kontuzja> Kontuzja { get; set; }

        public DbSet<AsystentTrenera2.Models.PozycjaRys> PozycjaRys { get; set; }

        public DbSet<AsystentTrenera2.Models.Aktualnosc> Aktualnosc { get; set; }

        public DbSet<AsystentTrenera2.Models.OcenaInnych> OcenaInnych { get; set; }

        
    }
}