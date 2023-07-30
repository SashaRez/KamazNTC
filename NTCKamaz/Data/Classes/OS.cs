using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class OS
    {

        [Key]
        public int ID { get; set; }

        [Column("Название ОС")]
        public string? OCName { get; set; }

        [Column("ID ПК")]
        public string? pcID { get; set; }

    }
}
