using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.ApplicationCore.Entities
{
    public class Item : BaseEntity
    {
        /** item name **/
        public String Name { get; set; }
        /** item cost price **/      
        public decimal CostPrice { get; set; } 
        /** item sell price **/
        public decimal SellPrice { get; set; }
        /** item quantity on stock **/
        public int Quantity { get; set; }       

       

    }
}
