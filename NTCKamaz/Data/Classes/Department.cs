using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class Department
    {
        [Key]
        public int ID { get; set; }

        [Column("Сокращённое наименование")]
        public string? ShortName { get; set; }

        [Column("Полное наименование")]
        public string? FullName { get; set; }

        [Column("ID Начальника")]
        public int? ManagerID { get; set; }
    }
}
