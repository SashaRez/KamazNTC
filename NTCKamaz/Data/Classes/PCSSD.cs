using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCSSD
    {
        [Key]
        public int ID { get; set; }

        [Column("SSD")]
        public string? SSD { get; set; }
    }
}