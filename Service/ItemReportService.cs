using System.Collections.Generic;

namespace InventoryManagement.Service.Services
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
