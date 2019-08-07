using InventoryManagement.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.ApplicationCore.Specifications
{
   public  class ITemSpecification : BaseSpecification<Item>
    {

        public ITemSpecification(int?id, String searchTerm) : base(i => i.Name.Contains(searchTerm))
        {
            ApplyOrderBy(i => i.Name);
        }
        public ITemSpecification(String itemName) : base(i => i.Name == itemName)
        {
            ApplyOrderBy(i => i.Name);
        }
    }
}
