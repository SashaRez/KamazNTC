using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NTCKamaz.Data.Clasees
{
    public class User
    {

        [Key]
        public int ID { get; set; }

        [Column("ФИО")]
        public string? UserName { get; set; }

        [Column("Табельный номер")]
        public string? TableNumber { get; set; }

        [Column("Кабинет")]
        public string? Cabinet { get; set; }

        [Column("ID должности")]
        public int? IDPosition { get; set; }

        [Column("ID отдела")]
        public int? IDDepartment { get; set; }

        [Column("Учётная запись")]
        public string? Account { get; set; }

        [Column("ID ПК")]
        public string? pcID { get; set; }

    }
}
