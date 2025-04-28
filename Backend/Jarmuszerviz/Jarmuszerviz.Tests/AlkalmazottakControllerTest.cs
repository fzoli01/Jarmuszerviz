using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Jarmuszerviz.Controllers;
using Jarmuszerviz.Models;
using Jarmuszerviz.Database;
using System.Data.Entity;

namespace Jarmuszerviz.Tests.Controllers
{
    [TestClass]
    public class AlkalmazottakControllerTests
    {
        private Mock<JarmuSzervizContext> _mockContext;
        private Mock<DbSet<Alkalmazottak>> _mockSet;
        private AlkalmazottakController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockSet = new Mock<DbSet<Alkalmazottak>>();
            _mockContext = new Mock<JarmuSzervizContext>();
            _controller = new AlkalmazottakController(_mockContext.Object);
        }

        [TestMethod]
        public void Get_ReturnsAllAlkalmazottak()
        {

            var data = new List<Alkalmazottak>
            {
                new Alkalmazottak { AlkalmazottID = 1, Nev = "Teszt1" },
                new Alkalmazottak { AlkalmazottID = 2, Nev = "Teszt2" }
            }.AsQueryable();

            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Alkalmazottak).Returns(_mockSet.Object);


            var result = _controller.Get() as OkNegotiatedContentResult<List<Alkalmazottak>>;


            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_WithId_ReturnsCorrectAlkalmazott()
        {

            var alkalmazott = new Alkalmazottak { AlkalmazottID = 1, Nev = "Teszt" };
            var data = new List<Alkalmazottak> { alkalmazott }.AsQueryable();

            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Alkalmazottak).Returns(_mockSet.Object);


            var result = _controller.Get(1) as OkNegotiatedContentResult<Alkalmazottak>;


            Assert.IsNotNull(result);
            Assert.AreEqual("Teszt", result.Content.Nev);
        }

        [TestMethod]
        public void Post_InvalidRequest_ReturnsBadRequest()
        {

            var request = new AlkalmazottakController.AlkalmazottakRequest
            {
                Nev = "",
                Jelszo = ""
            };


            var result = _controller.Post(request);


            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Delete_ExistingAlkalmazott_ReturnsNoContent()
        {

            var alkalmazott = new Alkalmazottak { AlkalmazottID = 1 };
            var data = new List<Alkalmazottak> { alkalmazott }.AsQueryable();

            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Alkalmazottak>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Alkalmazottak).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);


            var result = _controller.Delete(1);


            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
        }
    }
}
