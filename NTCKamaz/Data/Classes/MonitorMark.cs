
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class MonitorMark
    {
        [Key]
        public int ID { get; set; }

        [Column("Марка")]
        public string? markMonitor { get; set; }

    }
}