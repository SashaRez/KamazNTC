using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NTCKamaz.Data.Clasees
{
    public class Position
    {
        [Key]
        public int ID { get; set; }

        [Column("Наименование")]
        public string? positionName { get; set; }
    }
}
