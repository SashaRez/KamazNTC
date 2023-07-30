using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCVideocard
    {
        [Key]
        public int ID { get; set; }

        [Column("Видеокарта")]
        public string? graphicsCard { get; set; }
    }
}
