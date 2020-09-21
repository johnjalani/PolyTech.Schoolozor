using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoolozor.Model.ViewModel
{
    public class SalesOrderDetail
    {
        [DisplayName("SR")]
        public int Sr { get; set; }
        [DisplayName("Order Track Number")]
        public string OrderTrackNumber { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Special Offer")]
        public string SpecialOffer { get; set; }
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
        [DisplayName("Unit Price Discount")]
        public double UnitPriceDiscount { get; set; }
    }
}
