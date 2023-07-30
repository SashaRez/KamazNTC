using Microsoft.EntityFrameworkCore;
using NTCKamaz.Data.Clasees;
using NTCKamaz.Data.Classes;

namespace NTCKamaz.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<PC> Computers { get; set; }

        public DbSet<PCMark> PCMarks { get; set; }

        public DbSet<PCAssembly> Assemblies { get; set; }

        public DbSet<MonitorMark> MonitorMarks { get; set; }

        public DbSet<PCMonitor> Monitors { get; set; }

        public DbSet<PCProcessor> Processors { get; set; }

        public DbSet<PCMotherboard> Motherboards { get; set; }

        public DbSet<Chief> Chiefs { get; set; }

        public DbSet<PCRam> RAMS { get; set; }

        public DbSet<PowerSupplie> PowerSupplies { get; set; }

        public DbSet<PCVideocard> Videocards { get; set; }

        public DbSet<PCHDD> HDDs { get; set; }

        public DbSet<PCSSD> SSDs { get; set; }

        public DbSet<KeyboardsAndMouse> KeyboardsAndMouses { get; set; }

        public DbSet<Webcam> Webcams { get; set; }

        public DbSet<Headphone> Headphones { get; set; }

        public DbSet<NetworkFilter> NetworkFilters { get; set; }

        public DbSet<AdditionalDevice> AdditionalDevices { get; set; }

        public DbSet<InventoryNumber> InventoryNumbers { get; set; }

        public DbSet<PCComponent> PСComponents { get; set; }
        
        public DbSet<PCProvider> PСProviders { get; set; }

        public DbSet<PCCategory> PCCategories { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<OS> OC { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MOL> MOLs { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Printer> Printers { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
