using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class AdditionalDevice
    {
        [Key]
        public int? ID { get; set; }

        [Column("ID Клавиатуры+Мыши")]
        public int? keyboardMouseID { get; set; }

        [Column("ID Веб-камеры")]
        public int? webcamID { get; set; }

        [Column("ID Наушников")]
        public int? headphonesID { get; set; }

        [Column("ID Сетевого фильтра")]
        public int? networkFilterID { get; set; }

        [Column("ID ПК")]
        public string? pcID { get; set; }
    }
}