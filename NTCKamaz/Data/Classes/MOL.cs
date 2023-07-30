using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class MOL
    {
        [Key]
        public int ID { get; set; }

        [Column("ФИО")]
        public string? userName { get; set; }

        [Column("ID ПК")]
        public string? pcID { get; set; }
    }
}
