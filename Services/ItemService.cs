
using InventoryManagement.ApplicationCore.Entities;
using InventoryManagement.ApplicationCore.Interfaces;
using InventoryManagement.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Service
{
    public class ItemService : IItemService
    {
        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IAppLogger<ItemService> _logger;       

        public ItemService(IAsyncRepository<Item> itemRepository, IAppLogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
            //test


        }
       
        public async Task<List<Item>> ListAll()
        {
            return await _itemRepository.ListAllAsync(new ITemSpecification(null, ""));           
            //return _result;
        }
        public async Task<Item> CreateItemAsync(Item item)
        {
            var _result = await _itemRepository.ListAllAsync(new ITemSpecification(item.Name));           
            if (_result.Count == 0)
            {               
                await _itemRepository.AddAsync(item);
            }
            else
            {
                _logger.LogInformation($"Item already exist for {item.Name}");
                throw new Exception("Item Already Exists");
            }
            return item;
        }
        public async Task<Item> deleteAsync(String itemName)
        {
            var _result = await _itemRepository.ListAllAsync(new ITemSpecification(itemName));
          
            if (_result.Count > 0)
            {
                var _item = _result[0];              
                await _itemRepository.DeleteAsync(_item);
            }
            else
            {
                _logger.LogInformation($"Item Not Found for {itemName}");
            }
            return _result[0];

        }

        public async Task<List<Item>> Report()
        {
           
            var listItem = await _itemRepository.ListAllAsync(new ITemSpecification(null, ""));
            if (!listItem.Any())
            {
                _logger.LogInformation("No data found");
            }
            return listItem;
        }    

        public async Task<Item> UpdateBuy(String ItemName, int Quantity)
        {
            var _result = await _itemRepository.ListAllAsync(new ITemSpecification(ItemName));            
            if (_result.Count > 0)
            {
                var _item = _result[0];
                _item.Quantity = _item.Quantity + Quantity;
                await _itemRepository.UpdateAsync(_item);
            }
            else
            {
                _logger.LogInformation($"Item Not Found for {ItemName}");
            }
            return _result[0];
        }
        public async Task<Item> UpdateSell(String ItemName, int Quantity)
        {           
            var _result = await _itemRepository.ListAllAsync(new ITemSpecification(ItemName));           
            if (_result.Count > 0)
            {
                var _item = _result[0];
                if (_item.Quantity >= Quantity)
                {
                    _item.Quantity = _item.Quantity - Quantity;                 
                    await _itemRepository.UpdateAsync(_item);
                }
                else
                {
                    _logger.LogInformation($"Item {ItemName} - Does not have enough quantity");
                }
            }
            else
            {
                _logger.LogInformation($"Item Not Found for {ItemName}");
            }
            return _result[0];

        }
      
        public async Task<Item> UpdateSellPrice(String ItemName, decimal sellPrice)
        {
            var _result = await _itemRepository.ListAllAsync(new ITemSpecification(ItemName));           
            if (_result.Count > 0)
            {
                var _item = _result[0];
                _item.SellPrice = sellPrice;
                await _itemRepository.UpdateAsync(_item);
            }
            else
            {
                _logger.LogInformation($"Item Not Found for {ItemName}");
            }
            return _result[0];
        }
    }
}
