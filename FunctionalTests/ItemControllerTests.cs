using NUnit.Framework;
using Moq;
using InventoryManagement.ApplicationCore.Interfaces;
using InventoryManagement.ApplicationCore.Entities;
using InventoryManagement.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.tests.UnitTests
{
    [TestFixture]
    public class ItemControllerTests
    {
        private  Mock<IItemService> _mockitemServiceRepository;
        private Mock<IAsyncRepository<ReportHistory>> _mockreportHistoryRepository;
        private Mock<IAsyncRepository<Command>> _mockcommandTypesRepository;

        public ItemControllerTests()
        {
           
            _mockitemServiceRepository = new Mock<IItemService>();
            _mockreportHistoryRepository = new Mock<IAsyncRepository<ReportHistory>>();
            _mockcommandTypesRepository = new Mock<IAsyncRepository<Command>>();
            ItemControllerCreateItemshouldReturnOKWithResult();
        }

        [Test]
        public void ItemControllerTestsExists()        {
         
            var controller = new ItemController(_mockitemServiceRepository.Object, _mockreportHistoryRepository.Object, _mockcommandTypesRepository.Object);
            Assert.IsInstanceOf(typeof(ItemController), controller);
        }

       [Test]
        public void ItemControllerCreateItemshouldReturnOKWithResult()
        {          
            var controller = new ItemController(_mockitemServiceRepository.Object, _mockreportHistoryRepository.Object, _mockcommandTypesRepository.Object);
            var actionResult = controller.Create(new Item() { Name = "Test01", CostPrice = 10.0m, SellPrice = 15.0m, Quantity = 0, Id = 1 });
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void shouldThrownItemExceptionWhenCreateNewItemHasExistingItem()
        {
            var controller = new ItemController(_mockitemServiceRepository.Object, _mockreportHistoryRepository.Object, _mockcommandTypesRepository.Object);
            var actionResult = controller.Create(new Item() { Name = "Test01", CostPrice = 10.0m, SellPrice = 15.0m, Quantity = 0, Id = 1 });
            var okResult = actionResult.Result as OkObjectResult;
            Assert.AreNotEqual(200, okResult.Value);       
        }
        [Test]
        public void shouldDeleteItemSuccessfully()
        {

            var controller = new ItemController(_mockitemServiceRepository.Object, _mockreportHistoryRepository.Object, _mockcommandTypesRepository.Object);
            var actionResult = controller.Delete("Test01");
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void shouldUpdateBuySuccessfully()
        {
            var controller = new ItemController(_mockitemServiceRepository.Object, _mockreportHistoryRepository.Object, _mockcommandTypesRepository.Object);
            var actionResult = controller.UpdateBuy("Test01",new ItemUpdateDto() { Quantity=10 });
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);          
            Assert.AreEqual(200, okResult.StatusCode);           
        }
        [Test]
        public void shouldUpdateSellSuccessfully()
        {

            var controller = new ItemController(_mockitemServiceRepository.Object, _mockreportHistoryRepository.Object, _mockcommandTypesRepository.Object);
            var actionResult = controller.UpdateSell("Test01", new ItemUpdateDto() { Quantity = 2 });
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);          
        }
        [Test]
        public void shouldShowReportSuccessfully()
        {
            var controller = new ItemController(_mockitemServiceRepository.Object, _mockreportHistoryRepository.Object, _mockcommandTypesRepository.Object);
            var actionResult1 = controller.UpdateBuy("Test01", new ItemUpdateDto() { Quantity = 10 });
            var actionResult2 = controller.UpdateBuy("Test02", new ItemUpdateDto() { Quantity = 50 });
            var actionResult3 = controller.UpdateBuy("Test03", new ItemUpdateDto() { Quantity = 80 });
            var actionReport1 = controller.GenerateReport();
            var actionResult4 = controller.UpdateSell("Test01", new ItemUpdateDto() { Quantity = 5 });
            var actionResult5 = controller.UpdateSell("Test02", new ItemUpdateDto() { Quantity = 25 });
            var actionResult6 = controller.UpdateSell("Test03", new ItemUpdateDto() { Quantity = 40 });
            var actionReport2 = controller.GenerateReport();
        }

    }
}
