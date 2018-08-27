using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POPS.Models;
using POPSAPI;
using POPSAPI.Controllers;
using POPSAPI.Repositories;
using Moq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Pops.Tests
{
    [TestClass]
    public class ItemControllerTests
    {
        ItemsController controller;
        Mock<IItemRepository> mockItemRepository;

        [TestInitialize]
        public void Setup()
        {
            mockItemRepository = new Mock<IItemRepository>();
            controller = new ItemsController(mockItemRepository.Object);

        }

        [TestMethod]
        public void GetAllSuppliers_ShouldReturnAllSuppliers()
        {
            var testItems= GetTestItems();
            mockItemRepository.Setup(s => s.GetAllItems()).Returns(Task.FromResult(testItems).Result);
            var result = controller.GetITEMs() as List<ITEMModel>;
            Assert.AreEqual(testItems.Count, result.Count);
        }
        [TestMethod]
        public void GetSuppliersByNo_ShouldReturnSelectedSupplier()
        {
            var testItems = GetTestItems();
            mockItemRepository.Setup(s => s.GetItemByItemCode(It.IsAny<string>())).Returns(Task.FromResult(testItems[1]).Result);
            var result = controller.GetITEM("2");
            Assert.AreEqual(testItems[1].ITCODE, result);
        }

        [TestMethod]
        public void AddSupplierTest()
        {
            ITEMModel model = new ITEMModel { ITCODE = "1"};
            mockItemRepository.Setup(s => s.AddItem(It.IsAny<ITEMModel>())).Returns(Task.FromResult(model.ITCODE).Result);
            var result = controller.PostITEM(model);
            Assert.AreEqual(model.ITCODE, result);
        }

        [TestMethod]
        public void UpdateSupplierTest()
        {
            ITEMModel model = new ITEMModel { ITCODE = "1" };
            mockItemRepository.Setup(s => s.UpdateItem(It.IsAny<ITEMModel>())).Returns(Task.FromResult(model.ITCODE).Result);
            var result = controller.PutITEM(model.ITCODE, model);
            Assert.AreEqual(model.ITCODE, result);
        }

        [TestMethod]
        public void DeleteSupplierTest()
        {
            ITEMModel model = new ITEMModel { ITCODE = "1" };
            mockItemRepository.Setup(s => s.DeleteItem(It.IsAny<string>()));
            var result = controller.DeleteITEM(model.ITCODE);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private List<ITEMModel> GetTestItems()
        {
            var testItems = new List<ITEMModel>();
            testItems.Add(new ITEMModel { ITCODE = "1" });
            testItems.Add(new ITEMModel { ITCODE = "2" });
            testItems.Add(new ITEMModel { ITCODE = "3" });
            testItems.Add(new ITEMModel { ITCODE = "4"});

            return testItems;
        }
    }
}
