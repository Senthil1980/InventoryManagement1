using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Web.ViewModels
{
    public class ItemReportSummaryViewModel
    {
        public decimal TotalValue { get; set; }
        public decimal ProfitSincePreviousReport { get; set; }

        public List<ItemReportViewModel> itemReportViewModels { get; set; } = new List<ItemReportViewModel>();
    }
}
