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
    public class POMasterControllerTests
    {
       PoMastersController controller;
        Mock<IPoMasterRepository> mockPoMasterRepository;

        [TestInitialize]
        public void Setup()
        {
            mockPoMasterRepository = new Mock<IPoMasterRepository>();
            controller = new PoMastersController(mockPoMasterRepository.Object);

        }

        [TestMethod]
        public void GetAllPoMaster_ShouldReturnAllSuppliers()
        {
            var testPoMasters = GetTestPoMasters();
            mockPoMasterRepository.Setup(s => s.GetAllPOMasters()).Returns(Task.FromResult(testPoMasters).Result);
            var result = controller.GetPOMASTERs() as List<PoMasterModel>;
            Assert.AreEqual(testPoMasters.Count, result.Count);
        }
        [TestMethod]
        public void GetPoMasterByNo_ShouldReturnSelectedSupplier()
        {
            var testPoMasters = GetTestPoMasters();
            mockPoMasterRepository.Setup(s => s.GetPoMasterById(It.IsAny<string>())).Returns(Task.FromResult(testPoMasters[1]).Result);
            var result = controller.GetPOMASTER("2") as List<SupplierModel>;
            Assert.AreEqual(testPoMasters[1], result);
        }

        [TestMethod]
        public void AddPoMasterTest()
        {
            PoMasterModel model = new PoMasterModel { PONO = "1" };
            mockPoMasterRepository.Setup(s => s.AddPoMaster(It.IsAny<PoMasterModel>())).Returns(Task.FromResult(model.PONO).Result);
            var result = controller.PostPOMASTER(model);
            Assert.AreEqual(model.SUPLNO, result);
        }

        [TestMethod]
        public void UpdatePoMasterTest()
        {
            PoMasterModel model = new PoMasterModel { PONO = "1"};
            mockPoMasterRepository.Setup(s => s.UpdatePoMaster(It.IsAny<PoMasterModel>())).Returns(Task.FromResult(model.PONO).Result);
            var result = controller.PutPOMASTER(model.PONO, model);
            Assert.AreEqual(model.SUPLNO, result);
        }

        [TestMethod]
        public void DeletePoMasterTest()
        {
            PoMasterModel model = new PoMasterModel { SUPLNO = "1" };
            mockPoMasterRepository.Setup(s => s.DeletePoMaster(It.IsAny<string>()));
            var result = controller.DeletePOMASTER(model.SUPLNO);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private List<PoMasterModel> GetTestPoMasters()
        {
            var poMasterModels = new List<PoMasterModel>();
            poMasterModels.Add(new PoMasterModel { PONO = "1"  });
            poMasterModels.Add(new PoMasterModel { PONO = "2"});
            poMasterModels.Add(new PoMasterModel { PONO = "3"});
            poMasterModels.Add(new PoMasterModel { PONO = "4" });

            return poMasterModels;
        }
    }
}
