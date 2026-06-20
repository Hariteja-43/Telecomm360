using NUnit.Framework;
using Telecomm360.Controllers;
using Microsoft.AspNetCore.Mvc;
using Telecomm360.DTO;

namespace Telecomm360.Test.ControllerTest
{
    [TestFixture]
    public class DashboardControllerTests
    {
        private DashboardController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new DashboardController();
        }

        #region GetDashboard

        [Test]
        public void GetDashboard_Always_ReturnsOk()
        {
            var result = _controller.GetDashboard();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetDashboard_ReturnsDashboardDto()
        {
            var result = _controller.GetDashboard() as OkObjectResult;

            Assert.That(result.Value, Is.TypeOf<DashboardDto>());
        }

        [Test]
        public void GetDashboard_Data_NotNull()
        {
            var result = _controller.GetDashboard() as OkObjectResult;

            Assert.That(result.Value, Is.Not.Null);
        }

        [Test]
        public void GetDashboard_EmptyData_ReturnsZeroValues()
        {
            var result = _controller.GetDashboard() as OkObjectResult;
            var data = result.Value as DashboardDto;

            Assert.That(data.Subscribers, Is.EqualTo(0));
            Assert.That(data.ARPU, Is.EqualTo(0));
            Assert.That(data.ChurnRate, Is.EqualTo(0));
            Assert.That(data.KPIs, Does.Contain("Subscribers: 0"));
        }

        [Test]
        public void GetDashboard_Subscribers_IsZero()
        {
            var result = _controller.GetDashboard() as OkObjectResult;
            var data = result.Value as DashboardDto;

            Assert.That(data.Subscribers, Is.EqualTo(0));
        }

        [Test]
        public void GetDashboard_ARPU_IsZero()
        {
            var result = _controller.GetDashboard() as OkObjectResult;
            var data = result.Value as DashboardDto;

            Assert.That(data.ARPU, Is.EqualTo(0));
        }

        [Test]
        public void GetDashboard_ChurnRate_IsZero()
        {
            var result = _controller.GetDashboard() as OkObjectResult;
            var data = result.Value as DashboardDto;

            Assert.That(data.ChurnRate, Is.EqualTo(0));
        }

        [Test]
        public void GetDashboard_KPIs_NotEmpty()
        {
            var result = _controller.GetDashboard() as OkObjectResult;
            var data = result.Value as DashboardDto;

            Assert.That(data.KPIs, Is.Not.Empty);
        }

        #endregion
    }
}