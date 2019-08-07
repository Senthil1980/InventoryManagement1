using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Web.ViewModels
{
    public class ItemReportViewModel
    {

        public String ItemName { get; set; }
        public decimal BoughtAt { get; set; }
        public decimal SoldAt { get; set; }
        public int AvailableQty { get; set; }
        public decimal Value { get; set; }    

    }
}
