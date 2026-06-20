using NUnit.Framework;
using Moq;
using Telecomm360.Controllers;
using Telecomm360.Service.Interfaces;
using Telecomm360.DTOs;
using Telecomm360.DTO;
using Telecomm360.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class InvoiceControllerTests
    {
        private Mock<IInvoiceServices> _serviceMock;
        private InvoiceController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IInvoiceServices>();
            _controller = new InvoiceController(_serviceMock.Object);
        }

        #region Helpers

        private SearchDto CreateSearch() => new SearchDto();

        private InvoiceDto CreateDto(int id = 1)
        {
            return new InvoiceDto
            {
                Id = id
            };
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_Valid_ReturnsOk()
        {
            var data = new List<InvoiceDto> { CreateDto() };

            _serviceMock.Setup(s => s.GetAllInvoice(It.IsAny<SearchDto>()))
                        .Returns(data);

            var result = _controller.GetAll(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetAllInvoice(It.IsAny<SearchDto>()))
                        .Returns(new List<InvoiceDto>());

            var result = _controller.GetAll(CreateSearch());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetAll_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetAllInvoice(It.IsAny<SearchDto>()))
                        .Returns(new List<InvoiceDto>());

            _controller.GetAll(CreateSearch());

            _serviceMock.Verify(s => s.GetAllInvoice(It.IsAny<SearchDto>()), Times.Once);
        }

        [Test]
        public void GetAll_ReturnsCorrectData()
        {
            var data = new List<InvoiceDto> { CreateDto() };

            _serviceMock.Setup(s => s.GetAllInvoice(It.IsAny<SearchDto>()))
                        .Returns(data);

            var result = _controller.GetAll(CreateSearch()) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(data));
        }

        #endregion

        #region CreateInvoice

        [Test]
        public void CreateInvoice_Valid_ReturnsCreated()
        {
            _serviceMock.Setup(s => s.CreateInvoice(It.IsAny<InvoiceDto>()))
                        .Returns(CreateDto());

            var result = _controller.CreateInvoice(CreateDto());

            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
        }

        [Test]
        public void CreateInvoice_Null_ReturnsBadRequest()
        {
            var result = _controller.CreateInvoice(null);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void CreateInvoice_ServiceReturnsNull_ReturnsBadRequest()
        {
            _serviceMock.Setup(s => s.CreateInvoice(It.IsAny<InvoiceDto>()))
                        .Returns((InvoiceDto)null);

            var result = _controller.CreateInvoice(CreateDto());

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void CreateInvoice_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateInvoice(It.IsAny<InvoiceDto>()))
                        .Returns(CreateDto());

            _controller.CreateInvoice(CreateDto());

            _serviceMock.Verify(s => s.CreateInvoice(It.IsAny<InvoiceDto>()), Times.Once);
        }

        #endregion

        #region GetInvoiceById

        [Test]
        public void GetInvoiceById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetInvoiceById(1))
                        .Returns(CreateDto());

            var result = _controller.GetInvoiceById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetInvoiceById_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.GetInvoiceById(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetInvoiceById_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetInvoiceById(1))
                        .Returns((InvoiceDto)null);

            var result = _controller.GetInvoiceById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void GetInvoiceById_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetInvoiceById(1))
                        .Returns(CreateDto());

            _controller.GetInvoiceById(1);

            _serviceMock.Verify(s => s.GetInvoiceById(1), Times.Once);
        }

        #endregion

        #region Adjust

        [Test]
        public void Adjust_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateInvoice(1, 100))
                        .Returns(CreateDto());

            var result = _controller.Adjust(1, 100);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Adjust_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.Adjust(0, 100);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Adjust_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.UpdateInvoice(1, 100))
                        .Returns((InvoiceDto)null);

            var result = _controller.Adjust(1, 100);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void Adjust_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.UpdateInvoice(1, 100))
                        .Returns(CreateDto());

            _controller.Adjust(1, 100);

            _serviceMock.Verify(s => s.UpdateInvoice(1, 100), Times.Once);
        }

        #endregion

        #region Finalize

        [Test]
        public void Finalize_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.FinalizeInvoice(1))
                        .Returns(CreateDto());

            var result = _controller.Finalize(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Finalize_InvalidId_ReturnsBadRequest()
        {
            var result = _controller.Finalize(0);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Finalize_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.FinalizeInvoice(1))
                        .Returns((InvoiceDto)null);

            var result = _controller.Finalize(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        #endregion

        #region GetByCustomer

        [Test]
        public void GetByCustomer_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetInvoiceByCustomer(It.IsAny<string>()))
                        .Returns(new List<InvoiceDto> { CreateDto() });

            var result = _controller.GetByCustomer("CUST001");

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetByCustomer_Empty_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetInvoiceByCustomer(It.IsAny<string>()))
                        .Returns(new List<InvoiceDto>());

            var result = _controller.GetByCustomer("CUST001");

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetByCustomer_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetInvoiceByCustomer(It.IsAny<string>()))
                        .Returns(new List<InvoiceDto>());

            _controller.GetByCustomer("CUST001");

            _serviceMock.Verify(s => s.GetInvoiceByCustomer(It.IsAny<string>()), Times.Once);
        }

        #endregion
    }
}
