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
    public class IdpontFoglalasokControllerTest
    {
        private Mock<JarmuSzervizContext> _mockContext;
        private Mock<DbSet<IdopontFoglalasok>> _mockSet;
        private IdopontFoglalasokController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockSet = new Mock<DbSet<IdopontFoglalasok>>();
            _mockContext = new Mock<JarmuSzervizContext>();
            _controller = new IdopontFoglalasokController(_mockContext.Object);
        }

        [TestMethod]
        public void Get_ReturnsAllFoglalasok()
        {

            var data = new List<IdopontFoglalasok>
            {
                new IdopontFoglalasok { IdoPontID = 1, UgyfelID = 1 },
                new IdopontFoglalasok { IdoPontID = 2, UgyfelID = 2 }
            }.AsQueryable();

            _mockSet.As<IQueryable<IdopontFoglalasok>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<IdopontFoglalasok>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<IdopontFoglalasok>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<IdopontFoglalasok>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(_mockSet.Object);

            _mockContext.Setup(c => c.IdopontFoglalasok).Returns(_mockSet.Object);


            var result = _controller.Get() as OkNegotiatedContentResult<List<IdopontFoglalasok>>;


            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Post_InvalidRequest_ReturnsBadRequest()
        {

            var invalidFoglalas = new IdopontFoglalasok { UgyfelID = 0 }; 


            var result = _controller.Post(invalidFoglalas);


            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Delete_ExistingFoglalas_ReturnsNoContent()
        {

            var foglalas = new IdopontFoglalasok { IdoPontID = 1 };
            _mockSet.Setup(m => m.Find(1)).Returns(foglalas);
            _mockContext.Setup(c => c.IdopontFoglalasok).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);


            var result = _controller.Delete(1);


            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
        }
    }
}