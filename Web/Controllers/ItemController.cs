using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.ApplicationCore.Entities;
using InventoryManagement.ApplicationCore.Interfaces;
using InventoryManagement.ApplicationCore.Specifications;
using InventoryManagement.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers
{
    [Route("api/[controller]/[action]")]   
    public class ItemController : Controller
    {
      
        private readonly IItemService _itemRepository;
        private readonly IAsyncRepository<ReportHistory> _reportHistoryRepository;
        private readonly IAsyncRepository<Command> _commandTypesRepository;

        public ItemController(IItemService itemRepository, IAsyncRepository<ReportHistory> reportHistoryRepository, IAsyncRepository<Command> commandTypesRepository)
        {
            _itemRepository = itemRepository;
            _reportHistoryRepository = reportHistoryRepository;
            _commandTypesRepository = commandTypesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommandTypes()
        {
            var commandTypes = await _commandTypesRepository.ListAllAsync();
            return Ok(commandTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _itemRepository.ListAll();
            return Ok(items);
        }

        // POST: api/Item
        [HttpGet]
        public async Task<IActionResult> GenerateReport()
        {
            
            var report = await _itemRepository.Report();
            var previousReport = (await _reportHistoryRepository.ListAllAsync()).LastOrDefault() ?? new ReportHistory();
            var itemReportSummaryViewModel = new ItemReportSummaryViewModel();
            foreach (Item i in report.Items)
            {
                var _itemReportViewModel = new ItemReportViewModel();
                _itemReportViewModel.ItemName = i.Name;
                _itemReportViewModel.BoughtAt = i.CostPrice;
                _itemReportViewModel.SoldAt = i.SellPrice;
                _itemReportViewModel.AvailableQty = i.Quantity;
                _itemReportViewModel.Value = i.CostPrice * i.Quantity;
                itemReportSummaryViewModel.itemReportViewModels.Add(_itemReportViewModel);
            }          
            itemReportSummaryViewModel.TotalValue = report.CurrentTotal();
            itemReportSummaryViewModel.ProfitSincePreviousReport = previousReport.CurrentProfit;
            var reporthistory = new ReportHistory();
            reporthistory.LastProfit = previousReport.CurrentProfit;
            reporthistory.LastTotal = report.CurrentTotal();
            reporthistory.CurrentProfit = 0;
            await _reportHistoryRepository.AddAsync(reporthistory);
            return Ok(itemReportSummaryViewModel);
        }
        // POST: api/Item
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Item newItem)
        {
            var item = await _itemRepository.CreateItemAsync(newItem);
            if (item == null)
            {
                return Ok(400);
            }
            return Ok(item);
        }

        [HttpPut("{itemName}")]
        public async Task<IActionResult> UpdateBuy(string itemName, [FromBody] ItemUpdateDto exItem)
        {
            try
            {
                var item = await _itemRepository.UpdateBuy(itemName, exItem.Quantity);
                return Ok(item);
            }
            catch (Exception exUpdateBuy)
            {
                return NotFound();
            }
        }

        // PUT: api/Item/5
        [HttpPut("{itemName}")]
        public async Task<IActionResult> UpdateSell(string itemName, [FromBody] ItemUpdateDto exItem)
        {
            try
            {
                var item = await _itemRepository.UpdateSell(itemName, exItem.Quantity); 
                if(item != null)
                {
                    var currentReport = (await _reportHistoryRepository.ListAllAsync()).LastOrDefault() ?? new ReportHistory();
                    currentReport.CurrentProfit += exItem.Quantity * (item.SellPrice - item.CostPrice);
                    var reporthistory = new ReportHistory();
                    reporthistory.CurrentProfit = currentReport.CurrentProfit;
                    reporthistory.LastProfit = currentReport.LastProfit;
                    reporthistory.LastTotal = currentReport.LastTotal;
                    await _reportHistoryRepository.AddAsync(reporthistory);
                }
                return Ok(item);
            }
            catch (Exception exUpdateSell)
            {
                return NotFound();
            }
        }
       

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{itemName}")]
        public async Task<IActionResult> Delete(string itemName)
        {
            try
            {
                var item = await _itemRepository.deleteAsync(itemName);               
                if (item != null)
                {
                    var currentReport = (await _reportHistoryRepository.ListAllAsync()).LastOrDefault() ?? new ReportHistory();
                    currentReport.CurrentProfit -= item.Quantity *  item.CostPrice;
                    var reporthistory = new ReportHistory();
                    reporthistory.CurrentProfit = currentReport.CurrentProfit;
                    reporthistory.LastProfit = currentReport.LastProfit;
                    reporthistory.LastTotal = currentReport.LastTotal;
                    await _reportHistoryRepository.AddAsync(reporthistory);
                }
                return Ok(item);
            }
            catch (Exception exDel)
            {
                return NotFound();
            }
        }
    }
}
