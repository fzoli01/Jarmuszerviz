using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;
using Jarmuszerviz.Controllers;
using Jarmuszerviz.Database;
using Jarmuszerviz.Models;

namespace Jarmuszerviz.Tests.Controllers
{
    [TestClass]
    public class UgyfelekControllerTest
    {
        private Mock<JarmuSzervizContext> _mockContext;
        private Mock<DbSet<Ugyfelek>> _mockSet;
        private UgyfelekController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockSet = new Mock<DbSet<Ugyfelek>>();
            _mockContext = new Mock<JarmuSzervizContext>();
            _controller = new UgyfelekController(_mockContext.Object);
        }

        [TestMethod]
        public void Get_WithId_ReturnsCorrectUgyfel()
        {

            var ugyfel = new Ugyfelek { UgyfelID = 1, Nev = "Teszt" };
            var data = new List<Ugyfelek> { ugyfel }.AsQueryable();

            _mockSet.As<IQueryable<Ugyfelek>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Ugyfelek>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Ugyfelek>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Ugyfelek>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Ugyfelek).Returns(_mockSet.Object);

            var result = _controller.Get(1) as OkNegotiatedContentResult<Ugyfelek>;

            Assert.IsNotNull(result);
            Assert.AreEqual("Teszt", result.Content.Nev);
        }

        [TestMethod]
        public void Put_InvalidId_ReturnsBadRequest()
        {

            var frissitettUgyfel = new Ugyfelek { UgyfelID = 2, Nev = "Teszt2" };


            var result = _controller.Put(1, frissitettUgyfel); 

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Delete_NonExistingUgyfel_ReturnsNotFound()
        {

            _mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns((Ugyfelek)null);
            _mockContext.Setup(c => c.Ugyfelek).Returns(_mockSet.Object);

            var result = _controller.Delete(99);


            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}