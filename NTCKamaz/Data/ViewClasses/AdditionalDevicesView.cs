using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class AdditionalDevicesView
    {
        public int ID { get; set; }

        [DisplayName("Клавиатура+Мышь")]
        public string? KeyboardAndMouse { get; set; }

        [DisplayName("Веб-камера")]
        public string? Webcam { get; set; }

        [DisplayName("Наушник")]
        public string? Headphone { get; set; }

        [DisplayName("Сетевой фильтр")]
        public string? NetworkFilter { get; set; }

        [DisplayName("ID ПК")]
        public string? pcID { get; set; }


    }
}