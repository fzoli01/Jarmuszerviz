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
    public class JavitasokControllerTests
    {
        private Mock<JarmuSzervizContext> _mockContext;
        private Mock<DbSet<Javitasok>> _mockSet;
        private JavitasokController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockSet = new Mock<DbSet<Javitasok>>();
            _mockContext = new Mock<JarmuSzervizContext>();
            _controller = new JavitasokController(_mockContext.Object);
        }

        [TestMethod]
        public void Get_ReturnsAllJavitasok()
        {

            var data = new List<Javitasok>
            {
                new Javitasok { JavitasID = 1, Leiras = "Olajcsere" },
                new Javitasok { JavitasID = 2, Leiras = "Fékcsere" }
            }.AsQueryable();

            _mockSet.As<IQueryable<Javitasok>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Javitasok>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Javitasok>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Javitasok>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(_mockSet.Object);

            _mockContext.Setup(c => c.Javitasok).Returns(_mockSet.Object);


            var result = _controller.Get() as OkNegotiatedContentResult<List<Javitasok>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }


        [TestMethod]
        public void Post_InvalidRequest_ReturnsBadRequest()
        {

            Javitasok ujJavitas = new Javitasok
            {
                Leiras = null 
            };


            var result = _controller.Post(ujJavitas);


            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Post_ValidRequest_ReturnsCreated()
        {

            Javitasok ujJavitas = new Javitasok
            {
                Leiras = "Kuplung csere"
            };

            _mockSet.Setup(m => m.Add(It.IsAny<Javitasok>())).Returns<Javitasok>(j => j);
            _mockContext.Setup(c => c.Javitasok).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);


            var result = _controller.Post(ujJavitas);


            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Javitasok>));
        }

        [TestMethod]
        public void Put_InvalidRequest_ReturnsBadRequest()
        {

            var frissitettJavitas = new Javitasok
            {
                JavitasID = 2, 
                Leiras = "Új leírás"
            };


            var result = _controller.Put(1, frissitettJavitas);

  
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Delete_ExistingJavitas_ReturnsNoContent()
        {

            var javitas = new Javitasok { JavitasID = 1 };

            _mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(javitas);
            _mockContext.Setup(c => c.Javitasok).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);


            var result = _controller.Delete(1);


            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
        }
    }
}
