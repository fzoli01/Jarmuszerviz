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
    public class AlkatreszekControllerTests
    {
        private Mock<JarmuSzervizContext> _mockContext;
        private Mock<DbSet<Alkatreszek>> _mockSet;
        private AlkatreszekController _controller;
        private List<Alkatreszek> _testData;

        [TestInitialize]
        public void SetUp()
        {

            _testData = new List<Alkatreszek>
            {
                new Alkatreszek { AlkatreszID = 1, AlkatreszNev = "Alkatrész1" },
                new Alkatreszek { AlkatreszID = 2, AlkatreszNev = "Alkatrész2" }
            };


            _mockSet = new Mock<DbSet<Alkatreszek>>();
            _mockSet.As<IQueryable<Alkatreszek>>().Setup(m => m.Provider).Returns(_testData.AsQueryable().Provider);
            _mockSet.As<IQueryable<Alkatreszek>>().Setup(m => m.Expression).Returns(_testData.AsQueryable().Expression);
            _mockSet.As<IQueryable<Alkatreszek>>().Setup(m => m.ElementType).Returns(_testData.AsQueryable().ElementType);
            _mockSet.As<IQueryable<Alkatreszek>>().Setup(m => m.GetEnumerator()).Returns(() => _testData.AsQueryable().GetEnumerator());


            _mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids =>
                _testData.FirstOrDefault(d => d.AlkatreszID == (int)ids[0]));


            _mockContext = new Mock<JarmuSzervizContext>();
            _mockContext.Setup(c => c.Alkatreszek).Returns(_mockSet.Object);


            _controller = new AlkatreszekController();
            typeof(AlkatreszekController)
     .GetField("ctx", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
     .SetValue(_controller, _mockContext.Object);
        }

        

        [TestMethod]
        public void Get_ReturnsAllAlkatreszek()
        {

            var result = _controller.Get() as OkNegotiatedContentResult<List<Alkatreszek>>;


            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_WithId_ReturnsCorrectAlkatresz()
        {

            var result = _controller.Get(1) as OkNegotiatedContentResult<Alkatreszek>;


            Assert.IsNotNull(result);
            Assert.AreEqual("Alkatrész1", result.Content.AlkatreszNev);
        }

        [TestMethod]
        public void Delete_ExistingAlkatresz_ReturnsNoContent()
        {

            var result = _controller.Delete(1);


            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            _mockSet.Verify(m => m.Remove(It.IsAny<Alkatreszek>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}