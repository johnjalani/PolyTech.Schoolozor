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
        public int sr { get; set; }
        [DisplayName("Order Track Number")]
        public string ordertracknumber { get; set; }
        [DisplayName("Quantity")]
        public int quantity { get; set; }
        [DisplayName("Product Name")]
        public string productname { get; set; }
        [DisplayName("Special Offer")]
        public string specialoffer { get; set; }
        [DisplayName("Unit Price")]
        public double unitprice { get; set; }
        [DisplayName("Unit Price Discount")]
        public double unitpricediscount { get; set; }
    }
}
