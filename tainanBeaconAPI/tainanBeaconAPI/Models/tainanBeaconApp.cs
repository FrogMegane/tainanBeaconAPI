namespace tainanBeaconAPI.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class TainanBeaconApp : DbContext
    {
        public TainanBeaconApp()
            : base("name=tainanBeaconApp")
        {
            Database.SetInitializer<TainanBeaconApp>(new CreateDatabaseIfNotExists<TainanBeaconApp>());
        }

        /*Entities*/
        public DbSet<App> Apps { get; set; }

        public DbSet<Hold> Holds { get; set; }//Relation

        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<ReferenceLink> Links { get; set; }
        public DbSet<Site> Sites { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<CheckPoint> CheckPoints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var app = modelBuilder.Entity<App>().ToTable("Apps");
            var holds= modelBuilder.Entity<Hold>().ToTable("Hold");//舉辦
            var exhibition = modelBuilder.Entity<Exhibition>().ToTable("Exhibitions");
            var links = modelBuilder.Entity<ReferenceLink>().ToTable("Links");
            var sites = modelBuilder.Entity<Site>().ToTable("Sites");
            var floors = modelBuilder.Entity<Floor>().ToTable("Floors");
            var checkPoint = modelBuilder.Entity<CheckPoint>().ToTable("CheckPoints");


            app.HasKey<int>(a=>a.Id);//APP PK
            holds.HasKey(c=>new {c.AppId,c.ExhibitionId});//relation's PK
            holds.HasRequired(h=>h.App).WithMany(a=>a.Holds).HasForeignKey(h=>h.AppId);//FK1
            holds.HasRequired(h=>h.Exhibition).WithMany(e=>e.Holded).HasForeignKey(h=>h.ExhibitionId);//FK2

            exhibition.HasKey<int>(ex=>ex.Id);//Exhibition PK
            links.HasKey(l => new { l.ExhibitionId,l.Link });//複合key
            links.HasRequired(l => l.Exhibition).WithMany(e=>e.RefLinks);//一對多
            //舉辦地
            exhibition.HasRequired(e => e.Site).WithMany(s => s.Exhibitions).HasForeignKey(e=>e.SiteId);
            sites.HasKey<int>(s=>s.Id);//PK


            //一個場地有多個樓層
            floors.HasKey(f => f.Id);
            floors.HasRequired(f => f.Site).WithMany(s => s.Floors).HasForeignKey(f=>f.SiteId);

            checkPoint.HasKey(cp=>new { cp.Id,cp.IsBeacon});
            checkPoint.HasRequired(cp => cp.Floor).WithMany(f => f.CheckPoints).HasForeignKey(cp=>cp.FloorId);

            //完成原本程序用
            base.OnModelCreating(modelBuilder);
        }
    }
    

    public class App
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String TreasureMap { get; set;}
        public DateTime LastModified { get; set;}

        public virtual ICollection<Hold> Holds { get; set; }
    }
    public class Hold
    {
        public int AppId { get; set; }
        public int ExhibitionId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        [JsonIgnore]
        public virtual App App { get; set; }
        [JsonIgnore]
        public virtual Exhibition Exhibition { get; set; }
    }
    public class Exhibition
    {
        public int Id { get; set;}
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        
        //時間
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime OpenTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime CloseTime { get; set; }

        public string TicketInfo { get; set; }
        public string Note { get; set; }
        public string Icon { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<ReferenceLink> RefLinks { get; set; }//一對多
        public virtual ICollection<Hold> Holded { get; set; }//many to many
        public virtual Site Site { get; set; }
    }
    public class ReferenceLink {
        [Key, Column(Order = 1)]
        public int ExhibitionId { get; set; }
        [DataType(DataType.Url)]
        [Key, Column(Order = 2)]
        public string Link { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual Exhibition Exhibition { get; set; }
    }
    

    public class Site {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }

        public virtual ICollection<Exhibition> Exhibitions { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
    }


    public class Floor
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string FloorName { get; set; }
        public string FLoorMap { get; set; }

        public virtual Site Site { get; set; }
        public virtual ICollection<CheckPoint> CheckPoints { get; set; }
    }
    public class CheckPoint
    {
        public int Id { get; set; }//Beacon Id or QR Id
        public int FloorId { get; set; }
        public Boolean IsBeacon { get; set; }
        public string BeaconAPIId { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public virtual Floor Floor { get; set; }
    }
}