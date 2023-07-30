using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class OSView
    {
        public int ID { get; set; }

        [DisplayName("Название ОС")]
        public string? osName { get; set; }

        [DisplayName("ID ПК")]
        public string? pcID { get; set; }
    }
}