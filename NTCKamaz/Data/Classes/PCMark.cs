using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCMark
    {
        [Key]
        public int ID { get; set; }

        [Column("Марка")]
        public string? pcMark { get; set; }
    }
}
