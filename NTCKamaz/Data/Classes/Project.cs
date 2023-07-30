using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class Project
    {
        [Key]
        public int ID { get; set; }


        [Column("Проект")]
        public string? project { get; set; }

        [Column("ID ПК")]
        public string? pcID { get; set; }
    }
}