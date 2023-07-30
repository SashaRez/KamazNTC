using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCComponent
    {
        [Key]
        public int ID { get; set; }

        [Column("ID Процессора")]
        public int? processorID { get; set; }

        [Column("ID Мат.платы")]
        public int? motherboardID { get; set; }

        [Column("ID ОЗУ")]
        public int? ramID { get; set; }

        [Column("ID Видеокарты")]
        public int? graphicsCardID { get; set; }

        [Column("ID HDD")]
        public int? hddID { get; set; }

        [Column("ID SSD")]
        public int? ssdID { get; set; }

        [Column("ID БП")]
        public int? bpID { get; set; }

    }
}