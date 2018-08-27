using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POPS.Models;
using POPSAPI;
using POPSAPI.Controllers;
using POPSAPI.Repositories;
using Moq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace Pops.Tests
{
    [TestClass]
    public class PODetailControllerTests
    {
        PoDetailsController controller;
        Mock<IPoDetailRepository> mockPoDetailRepository;

        [TestInitialize]
        public void Setup()
        {
            mockPoDetailRepository = new Mock<IPoDetailRepository>();
            controller = new PoDetailsController(mockPoDetailRepository.Object);

        }

        [TestMethod]
        public void GetAllPoDetails_ShouldReturnAllSuppliers()
        {
            var testPoDetails = GetTestPoDetails();
            mockPoDetailRepository.Setup(s => s.GetAllPODetails()).Returns(Task.FromResult(testPoDetails).Result);
            var result = controller.GetPODETAILs() as List<PoDetailModel>;
            Assert.AreEqual(testPoDetails.Count, result.Count);
        }
        [TestMethod]
        public void GetSuppliersByNo_ShouldReturnSelectedSupplier()
        {
            var testPoMasters = GetTestPoDetails();
            mockPoDetailRepository.Setup(s => s.GetPoDetailsById(It.IsAny<string>())).Returns(Task.FromResult(testPoMasters[1]).Result);
            var result = controller.GetPODETAIL("2") as OkNegotiatedContentResult<PoDetailModel>;
            Assert.AreEqual(testPoMasters[1].PONO, result.Content.PONO);
        }

        [TestMethod]
        public void AddSupplierTest()
        {
            PoDetailModel model = new PoDetailModel { PONO = "1" };
            mockPoDetailRepository.Setup(s => s.AddPoDetail(It.IsAny<PoDetailModel>())).Returns(Task.FromResult(model.PONO).Result);
            var result = controller.PostPODETAIL(model) as CreatedAtRouteNegotiatedContentResult<PoDetailModel>;
            Assert.AreEqual(model.PONO, result.Content.PONO);
        }

        [TestMethod]
        public void UpdateSupplierTest()
        {
            PoDetailModel model = new PoDetailModel { PONO = "1" };
            mockPoDetailRepository.Setup(s => s.UpdatePoDetail(It.IsAny<PoDetailModel>())).Returns(Task.FromResult(model.PONO).Result);
            var result = controller.PutPODETAIL(model.PONO, model) as NegotiatedContentResult<PoDetailModel>;
            Assert.AreEqual(model.PONO, result.Content.PONO);
        }

        [TestMethod]
        public void DeleteSupplierTest()
        {
            PoDetailModel model = new PoDetailModel { PONO = "1" };
            mockPoDetailRepository.Setup(s => s.GetPoDetailsById(It.IsAny<string>())).Returns(model);
            var result = controller.DeletePODETAIL(model.PONO);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private List<PoDetailModel> GetTestPoDetails()
        {
            var poDetailModels = new List<PoDetailModel>();
            poDetailModels.Add(new PoDetailModel { PONO = "1" });
            poDetailModels.Add(new PoDetailModel { PONO = "2" });
            poDetailModels.Add(new PoDetailModel { PONO = "3" });
            poDetailModels.Add(new PoDetailModel { PONO = "4" });

            return poDetailModels;
        }
    }
}

