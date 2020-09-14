using System.Collections.Generic;

namespace FuelServices.DBContext.Domain
{
    public partial class ColorPaletteViewModel : BaseDomain
    {
        public ColorPaletteViewModel()
        {
            PaymentPackage = new HashSet<PaymentPackageViewModel>();
        }

        public string Name { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string Color4 { get; set; }
        public string Color5 { get; set; }

        public virtual ICollection<PaymentPackageViewModel> PaymentPackage { get; set; }
    }
}