using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class KeyboardsAndMouse
    {
        [Key]
        public int ID { get; set; }

        [Column("Клавиатура+Мышь")]
        public string? keyboardMouse { get; set; }
    }
}