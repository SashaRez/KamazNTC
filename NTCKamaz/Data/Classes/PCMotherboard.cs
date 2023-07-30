using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCMotherboard
    {
        [Key]
        public int ID { get; set; }

        [Column("Мат.плата")]
        public string? motherboardName { get; set; }
    }
}
