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
    public class JarmuvekControllerTest
    {
        private Mock<JarmuSzervizContext> _mockContext;
        private Mock<DbSet<Jarmuvek>> _mockSet;
        private JarmuvekController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockSet = new Mock<DbSet<Jarmuvek>>();
            _mockContext = new Mock<JarmuSzervizContext>();
            _controller = new JarmuvekController(_mockContext.Object);
        }

        [TestMethod]
        public void Get_ReturnsAllJarmuvek()
        {

            var data = new List<Jarmuvek>
            {
                new Jarmuvek { Alvazszam = "ABC123", Marka = "Toyota" },
                new Jarmuvek { Alvazszam = "XYZ789", Marka = "Honda" }
            }.AsQueryable();

            _mockSet.As<IQueryable<Jarmuvek>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Jarmuvek>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Jarmuvek>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Jarmuvek>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(_mockSet.Object);

            _mockContext.Setup(c => c.Jarmuvek).Returns(_mockSet.Object);


            var result = _controller.Get() as OkNegotiatedContentResult<List<Jarmuvek>>;


            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Post_ValidRequest_ReturnsCreated()
        {

            var ujJarmu = new Jarmuvek { Alvazszam = "NEW123" };
            _mockSet.Setup(m => m.Add(It.IsAny<Jarmuvek>())).Returns(ujJarmu);
            _mockContext.Setup(c => c.Jarmuvek).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Returns(1);


            var result = _controller.Post(ujJarmu);


            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Jarmuvek>));
        }
    }
}