using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using Jarmuszerviz.Controllers;
using Jarmuszerviz.Database;
using Jarmuszerviz.Models;

namespace Jarmuszerviz.Tests.Controllers
{
    [TestClass]
    public class FelhasznaltAlkatreszekControllerTests
    {
        private Mock<JarmuSzervizContext> _mockContext;
        private Mock<DbSet<FelhasznaltAlkatreszek>> _mockSet;
        private FelhasznaltAlkatreszekController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockSet = new Mock<DbSet<FelhasznaltAlkatreszek>>();

            var data = new List<FelhasznaltAlkatreszek>
            {
                new FelhasznaltAlkatreszek { JavitasID = 1, AlkatreszID = 10, Mennyiseg = 2 },
                new FelhasznaltAlkatreszek { JavitasID = 2, AlkatreszID = 20, Mennyiseg = 5 }
            }.AsQueryable();

            _mockSet.As<IQueryable<FelhasznaltAlkatreszek>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<FelhasznaltAlkatreszek>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<FelhasznaltAlkatreszek>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<FelhasznaltAlkatreszek>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(_mockSet.Object);

            _mockContext = new Mock<JarmuSzervizContext>();
            _mockContext.Setup(c => c.FelhasznaltAlkatreszek).Returns(_mockSet.Object);

            _controller = new FelhasznaltAlkatreszekController(_mockContext.Object);
        }

        [TestMethod]
        public void Get_ReturnsAllFelhasznaltAlkatreszek()
        {

            var result = _controller.Get() as OkNegotiatedContentResult<List<FelhasznaltAlkatreszek>>;


            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_WithIds_ReturnsCorrectFelhasznaltAlkatresz()
        {

            var result = _controller.Get(1, 10) as OkNegotiatedContentResult<FelhasznaltAlkatreszek>;


            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Mennyiseg);
        }

        [TestMethod]
        public void Post_InvalidRequest_ReturnsBadRequest()
        {

            var result = _controller.Post(null);

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Post_ValidRequest_ReturnsCreated()
        {

            var ujFelhasznaltAlkatresz = new FelhasznaltAlkatreszek
            {
                JavitasID = 3,
                AlkatreszID = 30,
                Mennyiseg = 4
            };

            _mockSet.Setup(m => m.Add(It.IsAny<FelhasznaltAlkatreszek>())).Returns<FelhasznaltAlkatreszek>(f => f);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);


            var result = _controller.Post(ujFelhasznaltAlkatresz);


            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<FelhasznaltAlkatreszek>));
        }

        [TestMethod]
        public void Put_InvalidRequest_ReturnsBadRequest()
        {

            var invalidUpdate = new FelhasznaltAlkatreszek
            {
                JavitasID = 5, 
                AlkatreszID = 50,
                Mennyiseg = 6
            };


            var result = _controller.Put(1, 10, invalidUpdate);


            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

    }
}
