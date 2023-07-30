using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PowerSupplie
    {
        [Key]
        public int? ID { get; set; }

        [Column("Блок питания")]
        public string? bpName { get; set; }
    }
}