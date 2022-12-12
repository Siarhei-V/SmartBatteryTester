using Microsoft.AspNetCore.Mvc;
using SmartBatteryTesterWebApp.UI.Controllers;
using Xunit;

namespace SmartBatteryTesterWebApp.UI.Tests
{
    public class AboutControllerTest
    {
        AboutController _aboutController;

        public AboutControllerTest()
        {
            _aboutController = new AboutController();
        }

        [Fact]
        public void Index_CheckReturnedValue()
        {
            // Arrange 
            
            // Act
            var result = _aboutController.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_CheckReturnedValue()
        {
            // Arrange 

            // Act
            var result = _aboutController.Error();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
