namespace tainanBeaconAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class tainanBeaconApp : DbContext
    {
        public tainanBeaconApp()
            : base("name=tainanBeaconApp")
        {
        }

        /*Entities*/
        public DbSet<App> Apps { get; set; }

        public DbSet<ReferenceLink> Holds { get; set; }//Relation

        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<ReferenceLink> Links { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<SetedIn> SetedIn { get; set; }//Relation

        public DbSet<CheckPoint> CheckPoints { get; set; }        
    }
    

    public class App
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] TreasureMap { get; set;}
        public DateTime LastModified { get; set;}
    }
    public class Hold
    {
        public int AppId { get; set; }
        public int ExhibitionId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
    public class Exhibition
    {
        public int Id { get; set;}
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string TicketInfo { get; set; }
        public string Note { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Photo { get; set; }
    }
    public class ReferenceLink {
        [Key]
        public int ExhibitionId { get; set; }
        [DataType(DataType.Url)]
        [Key]
        public string Link { get; set; }
        public string Name { get; set; }
    }
    

    public class Site {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
    }


    public class Floor
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string FloorName { get; set; }
        public byte[] FLoorMap { get; set; }

    }
    public class SetedIn {
        public int CheckPointId { get; set; }
        public int FloorId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
    public class CheckPoint
    {
        public int Id { get; set; }//Beacon Id or QR Id
        public Boolean IsBeacon { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}