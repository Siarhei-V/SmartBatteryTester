using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using System.Reflection;
using Xunit;

namespace SmartBatteryTesterDesktopApp.PORT.Tests
{
    public class PortDataHandlerTest
    {
        [Fact]
        public void CheckSingletonImplementation()
        {
            // Arrange
            IPortDataHandler firstPortDataHandler; 
            IPortDataHandler secondPortDataHandler;

            // Act
            firstPortDataHandler = PortDataHandler.Instance;
            secondPortDataHandler = PortDataHandler.Instance;

            // Assert
            Assert.Same(firstPortDataHandler, secondPortDataHandler);
        }

        [Fact]
        public void CheckNoPublicConstructors()
        {
            // Arrange
            Type portDataHandler = typeof(PortDataHandler);
            ConstructorInfo[] constructorInfos = portDataHandler.GetConstructors();
            bool hasPublicConstructors = false;

            // Act
            foreach (var item in constructorInfos)
            {
                if (item.IsPublic)
                {
                    hasPublicConstructors = true;
                    break;
                }
            }

            // Assert
            Assert.False(hasPublicConstructors);
        }
    }
}
