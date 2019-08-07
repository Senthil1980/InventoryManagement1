using InventoryManagement.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.ApplicationCore.Interfaces
{
    public interface IItemService
    {
        Task<IReadOnlyList<Item>> ListAll();
        Task<Item> UpdateBuy(String ItemName, int Quantity);
        Task<Item> UpdateSell(String ItemName, int Quantity);
        Task<Item> CreateItemAsync(Item item);
        Task<Item> deleteAsync(String  itemName);
        Task<ItemReportService> Report();     
    }
}
