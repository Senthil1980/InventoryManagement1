using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.ApplicationCore.Entities
{
    public class ReportHistory : BaseEntity
    {
        public decimal LastTotal { get; set; }
        public decimal LastProfit { get; set; }     
        public decimal CurrentProfit { get; set; }
    }
}
