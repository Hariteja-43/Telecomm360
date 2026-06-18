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

namespace Telecom360.Test.ControllerTest
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

        // ✅ FIXED HERE
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
            var orders = new List<OrderResponseDto>
            {
                CreateTestOrder(1),
                CreateTestOrder(2)
            };

            _serviceMock.Setup(s => s.GetAllOrders())
                        .ReturnsAsync(orders);

            var result = await _controller.GetAllOrders();

            var ok = result as OkObjectResult;

            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(orders));
        }

        #endregion

        #region GetOrderById

        [Test]
        public async Task GetOrderById_Valid_ReturnsOk()
        {
            var order = CreateTestOrder(1);

            _serviceMock.Setup(s => s.GetOrderById(1))
                        .ReturnsAsync(order);

            var result = await _controller.GetOrderById(1);

            var ok = result as OkObjectResult;

            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(order));
        }

        [Test]
        public async Task GetOrderById_NotFound_Returns404()
        {
            _serviceMock.Setup(s => s.GetOrderById(1))
                        .ReturnsAsync((OrderResponseDto)null);

            var result = await _controller.GetOrderById(1);

            var notFound = result as NotFoundObjectResult;

            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(ErrorMessages.NOT_FOUND));
        }

        #endregion

        #region CreateOrder

        [Test]
        public async Task CreateOrder_Valid_ReturnsCreated()
        {
            var request = CreateCreateOrderRequest();
            var response = CreateTestOrder(1);

            _serviceMock.Setup(s => s.CreateOrder(request))
                        .ReturnsAsync(response);

            var result = await _controller.CreateOrder(request);

            var created = result as CreatedAtActionResult;

            Assert.That(created, Is.Not.Null);
            Assert.That(created.Value, Is.EqualTo(response));
        }

        #endregion

        #region UpdateOrder

        [Test]
        public async Task UpdateOrder_Valid_ReturnsOk()
        {
            var request = CreateUpdateOrderRequest();
            var response = CreateTestOrder(1);

            _serviceMock.Setup(s => s.UpdateOrder(1, request))
                        .ReturnsAsync(response);

            var result = await _controller.UpdateOrder(1, request);

            var ok = result as OkObjectResult;

            Assert.That(ok, Is.Not.Null);
            Assert.That(ok.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task UpdateOrder_NotFound_Returns404()
        {
            var request = CreateUpdateOrderRequest();

            _serviceMock.Setup(s => s.UpdateOrder(1, request))
                        .ReturnsAsync((OrderResponseDto)null);

            var result = await _controller.UpdateOrder(1, request);

            var notFound = result as NotFoundObjectResult;

            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.Value, Is.EqualTo(ErrorMessages.NOT_FOUND));
        }

        #endregion

        #region CancelOrder

        [Test]
        public async Task CancelOrder_Valid_ReturnsCancelledResponse()
        {
            _serviceMock.Setup(s => s.CancelOrder(1))
                        .ReturnsAsync(true);

            var result = await _controller.CancelOrder(1);

            var ok = result as OkObjectResult;
            var data = ok.Value as OrderActionResponseDto;

            Assert.That(ok, Is.Not.Null);
            Assert.That(data, Is.Not.Null);
            Assert.That(data.Status, Is.EqualTo("CANCELLED"));
        }

        #endregion

        #region SubmitOrder

        [Test]
        public async Task SubmitOrder_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.SubmitOrder(1))
                        .ReturnsAsync(true);

            var result = await _controller.SubmitOrder(1);

            var ok = result as OkObjectResult;
            var data = ok.Value as OrderActionResponseDto;

            Assert.That(ok, Is.Not.Null);
            Assert.That(data.Status, Is.EqualTo("SUBMITTED"));
        }

        [Test]
        public async Task SubmitOrder_Invalid_ReturnsBadRequest()
        {
            _serviceMock.Setup(s => s.SubmitOrder(1))
                        .ReturnsAsync(false);

            var result = await _controller.SubmitOrder(1);

            var badRequest = result as BadRequestObjectResult;

            Assert.That(badRequest, Is.Not.Null);
        }

        #endregion

        #region FulfillOrder

        [Test]
        public async Task FulfillOrder_Valid_ReturnsOk()
        {
            _serviceMock.Setup(s => s.FulfillOrder(1))
                        .ReturnsAsync(true);

            var result = await _controller.FulfillOrder(1);

            var ok = result as OkObjectResult;
            var data = ok.Value as OrderActionResponseDto;

            Assert.That(ok, Is.Not.Null);
            Assert.That(data.Status, Is.EqualTo("FULFILLED"));
            Assert.That(data.OrderID, Is.EqualTo(1));
        }

        [Test]
        public async Task FulfillOrder_Invalid_ReturnsBadRequest()
        {
            _serviceMock.Setup(s => s.FulfillOrder(1))
                        .ReturnsAsync(false);

            var result = await _controller.FulfillOrder(1);

            var badRequest = result as BadRequestObjectResult;

            Assert.That(badRequest, Is.Not.Null);
        }

        #endregion
    }
}
