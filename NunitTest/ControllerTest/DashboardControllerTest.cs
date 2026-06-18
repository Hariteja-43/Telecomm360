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

        #region ✅ GetDashboard (3 Tests)

        [Test]
        public void GetDashboard_Always_ReturnsOk()
        {
            // Act
            var result = _controller.GetDashboard();

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test]
        public void GetDashboard_ReturnsDashboardDto()
        {
            // Act
            var result = _controller.GetDashboard();

            // Assert
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);

            var data = ok.Value as DashboardDto;
            Assert.That(data, Is.Not.Null);
        }

        [Test]
        public void GetDashboard_EmptyData_ReturnsZeroValues()
        {
            // Act
            var result = _controller.GetDashboard();

            var ok = result as OkObjectResult;
            var data = ok.Value as DashboardDto;

            // Assert
            Assert.That(data.Subscribers, Is.EqualTo(0));
            Assert.That(data.ARPU, Is.EqualTo(0));
            Assert.That(data.ChurnRate, Is.EqualTo(0));
            Assert.That(data.KPIs, Does.Contain("Subscribers: 0"));
        }

        #endregion
    }
}