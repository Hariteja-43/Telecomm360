using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.DTOs;
using Telecomm360.DTO;
using Telecomm360.Service.Interfaces;
using Telecomm360.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class PaymentControllerTests
    {
        private Mock<IPaymentService> _serviceMock;
        private PaymentController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IPaymentService>();
            _controller = new PaymentController(_serviceMock.Object);
        }

        #region Helpers

        private SearchDto CreateSearch() => new SearchDto();

        private PaymentDto CreateDto()
        {
            return new PaymentDto
            {
                PaymentID = 1
            };
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllPayment(It.IsAny<SearchDto>()))
                        .Returns(new List<PaymentDto> { CreateDto() });

            var result = _controller.GetAll(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllPayment(It.IsAny<SearchDto>()))
                        .Returns(new List<PaymentDto>());

            var result = _controller.GetAll(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetAllPayment(It.IsAny<SearchDto>()))
                        .Returns(new List<PaymentDto>());

            _controller.GetAll(CreateSearch());

            _serviceMock.Verify(s => s.GetAllPayment(It.IsAny<SearchDto>()), Times.Once);
        }

        [Test]
        public void GetAll_ReturnsCorrectData()
        {
            var data = new List<PaymentDto> { CreateDto() };

            _serviceMock.Setup(s => s.GetAllPayment(It.IsAny<SearchDto>()))
                        .Returns(data);

            var result = _controller.GetAll(CreateSearch()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(data));
        }

        #endregion

        #region Create

        [Test]
        public void Create_Valid_ReturnsCreated()
        {
            _serviceMock.Setup(s => s.CreatePayment(It.IsAny<PaymentDto>()))
                        .Returns(CreateDto());

            var result = _controller.Create(CreateDto());

            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
        }

        [Test]
        public void Create_NullPayload_ReturnsBadRequest()
        {
            var result = _controller.Create(null);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Create_ActionNameCheck()
        {
            _serviceMock.Setup(s => s.CreatePayment(It.IsAny<PaymentDto>()))
                        .Returns(CreateDto());

            var result = _controller.Create(CreateDto()) as CreatedAtActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Get"));
        }

        [Test]
        public void Create_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreatePayment(It.IsAny<PaymentDto>()))
                        .Returns(CreateDto());

            _controller.Create(CreateDto());

            _serviceMock.Verify(s => s.CreatePayment(It.IsAny<PaymentDto>()), Times.Once);
        }

        #endregion

        #region GetById

        [Test]
        public void Get_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetPaymentById(1))
                        .Returns(CreateDto());

            var result = _controller.Get(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Get_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.Get(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Get_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetPaymentById(1))
                        .Returns((PaymentDto)null);

            var result = _controller.Get(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void Get_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetPaymentById(1))
                        .Returns(CreateDto());

            _controller.Get(1);

            _serviceMock.Verify(s => s.GetPaymentById(1), Times.Once);
        }

        #endregion

        #region Reconcile

        [Test]
        public void Reconcile_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.Reconcile(1))
                        .Returns(CreateDto());

            var result = _controller.Reconcile(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Reconcile_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.Reconcile(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Reconcile_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.Reconcile(1))
                        .Returns((PaymentDto)null);

            var result = _controller.Reconcile(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void Reconcile_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.Reconcile(1))
                        .Returns(CreateDto());

            _controller.Reconcile(1);

            _serviceMock.Verify(s => s.Reconcile(1), Times.Once);
        }

        #endregion
    }
}