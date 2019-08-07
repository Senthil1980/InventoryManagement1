using InventoryManagement.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.ApplicationCore
{
    public class ItemReportService
    {      
        public decimal CurrentTotal()
        {
            decimal total = 0m;
            foreach (var item in Items)
            {
                total += item.CostPrice * item.Quantity;
            }
            return total;

        } 
        public List<Item> Items = new List<Item>();      


    }
}
