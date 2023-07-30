using NTCKamaz.Data.ViewClasses;

namespace NTCKamaz.Models
{
    public class Model
    {
        public List<PCView>? PCViews { get; set; }

        public List<ComponentView>? ComponentViews { get; set; }

        public List<MonitorView>? MonitorViews { get; set; }

        public List<OSView>? OSViews { get; set; }

        public List<MOLView>? MOLViews { get; set; }

        public List<AssemblyView>? AssemblyViews { get; set; }

        public List<UserView>? UserViews { get; set; }

        public List<DepartmentView>? DepartmentViews { get; set; }

        public List<InventoryNumberView>? InventoryNumberViews { get; set; }

        public List<AdditionalDevicesView>? AdditionalDevicesViews { get; set; }

        public List<PrinterView>? PrinterViews { get; set; }

    }
}
