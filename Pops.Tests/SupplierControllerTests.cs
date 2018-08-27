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
    public class TestSupplierController
    {
        SUPPLIERsController controller;
        Mock<ISupplierRepository> mockSupplierRepository;

        [TestInitialize]
        public void Setup()
        {
            mockSupplierRepository = new Mock<ISupplierRepository>();
            controller = new SUPPLIERsController(mockSupplierRepository.Object);

        }

        [TestMethod]
        public void GetAllSuppliers_ShouldReturnAllSuppliers()
        {
            var testSuppliers = GetTestSuppliers();
            mockSupplierRepository.Setup(s => s.GetAllSuppliers()).Returns(Task.FromResult(testSuppliers).Result);
            var result = controller.GetSUPPLIERs() as List<SupplierModel>;
            Assert.AreEqual(testSuppliers.Count, result.Count);
        }
        [TestMethod]
        public void GetSuppliersByNo_ShouldReturnSelectedSupplier()
        {
            var testSuppliers = GetTestSuppliers();
            mockSupplierRepository.Setup(s => s.GetSupplierById(It.IsAny<string>())).Returns(Task.FromResult(testSuppliers[1]).Result);
            var result = controller.GetSUPPLIER("2") as List<SupplierModel>;
            Assert.AreEqual(testSuppliers[1], result);
        }

        [TestMethod]
        public void AddSupplierTest()
        {
            SupplierModel model = new SupplierModel { SUPLNO = "1", SUPLNAME = "Supplier1", SUPLADDR = "SuppAddress1" };
            mockSupplierRepository.Setup(s => s.AddSupplier(It.IsAny<SupplierModel>())).Returns(Task.FromResult(model.SUPLNO).Result);
            var result = controller.PostSUPPLIER(model);
            Assert.AreEqual(model.SUPLNO, result);
        }

        [TestMethod]
        public void UpdateSupplierTest()
        {
            SupplierModel model = new SupplierModel { SUPLNO = "1", SUPLNAME = "Supplier1", SUPLADDR = "SuppAddress1" };
            mockSupplierRepository.Setup(s => s.UpdateSupplier(It.IsAny<SupplierModel>())).Returns(Task.FromResult(model.SUPLNO).Result);
            var result = controller.PutSUPPLIER(model.SUPLNO, model);
            Assert.AreEqual(model.SUPLNO, result);
        }

        [TestMethod]
        public void DeleteSupplierTest()
        {
            SupplierModel model = new SupplierModel { SUPLNO = "1", SUPLNAME = "Supplier1", SUPLADDR = "SuppAddress1" };
            mockSupplierRepository.Setup(s => s.DeleteSupplier(It.IsAny<string>()));
            var result = controller.DeleteSUPPLIER(model.SUPLNO);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private List<SupplierModel> GetTestSuppliers()
        {
            var testSuppliers = new List<SupplierModel>();
            testSuppliers.Add(new SupplierModel { SUPLNO = "1", SUPLNAME = "Supplier1", SUPLADDR = "SuppAddress1" });
            testSuppliers.Add(new SupplierModel { SUPLNO = "2", SUPLNAME = "Supplier2", SUPLADDR = "SuppAddress2" });
            testSuppliers.Add(new SupplierModel { SUPLNO = "3", SUPLNAME = "Supplier3", SUPLADDR = "SuppAddress3" });
            testSuppliers.Add(new SupplierModel { SUPLNO = "4", SUPLNAME = "Supplier4", SUPLADDR = "SuppAddress4" });

            return testSuppliers;
        }
    }
}
