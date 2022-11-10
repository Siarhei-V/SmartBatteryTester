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
            IPortInteractor firstPortDataHandler; 
            IPortInteractor secondPortDataHandler;

            // Act
            firstPortDataHandler = PortInteractor.Instance;
            secondPortDataHandler = PortInteractor.Instance;

            // Assert
            Assert.Same(firstPortDataHandler, secondPortDataHandler);
        }

        [Fact]
        public void CheckNoPublicConstructors()
        {
            // Arrange
            Type portDataHandler = typeof(PortInteractor);
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
