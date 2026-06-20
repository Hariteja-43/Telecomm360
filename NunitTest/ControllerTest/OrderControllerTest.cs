using NUnit.Framework;
using Moq;
using Telecom360.Controllers;
using Telecom360.Service.Interface;
using Telecom360.DTO.Order;
using Telecom360.Constant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class OrderControllerTests
    {
        private Mock<IOrderService> _serviceMock;
        private OrderController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IOrderService>();
            _controller = new OrderController(_serviceMock.Object);
        }

        #region Helpers

        private OrderResponseDto CreateTestOrder(int orderId = 1)
        {
            return new OrderResponseDto
            {
                OrderID = orderId,
                SubscriberID = 100,
                ProductID = 200,
                OrderDate = DateTime.UtcNow,
                Status = "CREATED",
                FulfillmentSteps = "INIT"
            };
        }

        private CreateOrderRequestDto CreateCreateOrderRequest()
        {
            return new CreateOrderRequestDto
            {
                SubscriberID = 100,
                ProductID = 200
            };
        }

        private UpdateOrderRequestDto CreateUpdateOrderRequest()
        {
            return new UpdateOrderRequestDto
            {
                Status = "UPDATED",
                FulfillmentSteps = "Updated Step"
            };
        }

        #endregion

        #region GetAllOrders

        [Test]
        public async Task GetAllOrders_Valid_ReturnsOk()
        {
            var orders = new List<OrderResponseDto> { CreateTestOrder() };

            _serviceMock.Setup(s => s.GetAllOrders())
                        .ReturnsAsync(orders);

            var result = await _controller.GetAllOrders();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAllOrders_ReturnsCorrectData()
        {
            var orders = new List<OrderResponseDto> { CreateTestOrder() };

            _serviceMock.Setup(s => s.GetAllOrders())
                        .ReturnsAsync(orders);

            var result = await _controller.GetAllOrders() as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(orders));
        }

        #endregion

        #region GetOrderById

        [Test]
        public async Task GetOrderById_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.GetOrderById(1))
                        .ReturnsAsync(CreateTestOrder());

            var result = await _controller.GetOrderById(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GetOrderById_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.GetOrderById(1))
                        .ReturnsAsync((OrderResponseDto)null);

            var result = await _controller.GetOrderById(1);

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetOrderById_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.GetOrderById(1))
                        .ReturnsAsync(CreateTestOrder());

            await _controller.GetOrderById(1);

            _serviceMock.Verify(s => s.GetOrderById(1), Times.Once);
        }

        #endregion

        #region CreateOrder

        [Test]
        public async Task CreateOrder_Valid_ReturnsCreated()
        {
            _serviceMock.Setup(s => s.CreateOrder(It.IsAny<CreateOrderRequestDto>()))
                        .ReturnsAsync(CreateTestOrder());

            var result = await _controller.CreateOrder(CreateCreateOrderRequest());

            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
        }

        [Test]
        public async Task CreateOrder_NullRequest_ThrowsNullReferenceException()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () => await _controller.CreateOrder(null));
        }

        [Test]
        public async Task CreateOrder_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CreateOrder(It.IsAny<CreateOrderRequestDto>()))
                        .ReturnsAsync(CreateTestOrder());

            await _controller.CreateOrder(CreateCreateOrderRequest());

            _serviceMock.Verify(s => s.CreateOrder(It.IsAny<CreateOrderRequestDto>()), Times.Once);
        }

        #endregion

        #region UpdateOrder

        [Test]
        public async Task UpdateOrder_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.UpdateOrder(1, It.IsAny<UpdateOrderRequestDto>()))
                        .ReturnsAsync(CreateTestOrder());

            var result = await _controller.UpdateOrder(1, CreateUpdateOrderRequest());

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateOrder_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.UpdateOrder(1, It.IsAny<UpdateOrderRequestDto>()))
                        .ReturnsAsync((OrderResponseDto)null);

            var result = await _controller.UpdateOrder(1, CreateUpdateOrderRequest());

            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task UpdateOrder_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.UpdateOrder(1, It.IsAny<UpdateOrderRequestDto>()))
                        .ReturnsAsync(CreateTestOrder());

            await _controller.UpdateOrder(1, CreateUpdateOrderRequest());

            _serviceMock.Verify(s => s.UpdateOrder(1, It.IsAny<UpdateOrderRequestDto>()), Times.Once);
        }

        #endregion

        #region CancelOrder

        [Test]
        public async Task CancelOrder_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.CancelOrder(1))
                        .ReturnsAsync(true);

            var result = await _controller.CancelOrder(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CancelOrder_ServiceCalledOnce()
        {
            _serviceMock.Setup(s => s.CancelOrder(1))
                        .ReturnsAsync(true);

            await _controller.CancelOrder(1);

            _serviceMock.Verify(s => s.CancelOrder(1), Times.Once);
        }

        #endregion

        #region SubmitOrder

        [Test]
        public async Task SubmitOrder_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.SubmitOrder(1))
                        .ReturnsAsync(true);

            var result = await _controller.SubmitOrder(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task SubmitOrder_Invalid_ReturnsBadRequest()
        {
            _serviceMock.Setup(s => s.SubmitOrder(1))
                        .ReturnsAsync(false);

            var result = await _controller.SubmitOrder(1);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        #endregion

        #region FulfillOrder

        [Test]
        public async Task FulfillOrder_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.FulfillOrder(1))
                        .ReturnsAsync(true);

            var result = await _controller.FulfillOrder(1);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task FulfillOrder_Invalid_ReturnsBadRequest()
        {
            _serviceMock.Setup(s => s.FulfillOrder(1))
                        .ReturnsAsync(false);

            var result = await _controller.FulfillOrder(1);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        #endregion
    }
}