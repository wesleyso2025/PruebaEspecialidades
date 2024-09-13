using LibeyTechnicalTestAPI.Controllers.LibeyUser;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace LibeiTechnicalTestTest
{
    [TestClass]
    public class Tests
    {
        private Mock<ILibeyUserAggregate> _mockAggregate;
        private LibeyUserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockAggregate = new Mock<ILibeyUserAggregate>();
            _controller = new LibeyUserController(_mockAggregate.Object);
        }

        [TestMethod]
        public void FindResponse_ReturnsOk_WhenUserExists()
        {
            var documentNumber = "123456";
            var expectedUserResponse = new LibeyUserResponse
            {
                DocumentNumber = documentNumber,
                Name = "Test Name"
            };
            _mockAggregate.Setup(x => x.FindResponse(documentNumber)).Returns(expectedUserResponse);

            var result = _controller.FindResponse(documentNumber) as OkObjectResult;

            
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedUserResponse, result.Value);
        }

        [TestMethod]
        public void FindResponse_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var documentNumber = "123456";
            _mockAggregate.Setup(x => x.FindResponse(documentNumber)).Returns((LibeyUserResponse)null);

            var result = _controller.FindResponse(documentNumber) as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual($"User with document number {documentNumber} not found.", result.Value);
        }
    }
}