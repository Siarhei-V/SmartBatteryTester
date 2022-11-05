using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using System.Reflection;
using Xunit;

namespace SmartBatteryTesterDesktopApp.PORT.Tests
{
    public class PortParametersSetterTest
    {
        [Fact]
        public void CheckSingletonImplementation()
        {
            // Arrange
            IPortParametersSetter firstPortParametersSetter = PortParametersSetter.Instance;
            IPortParametersSetter secondPortParametersSetter = PortParametersSetter.Instance;

            // Act

            // Arrange
            Assert.Equal(firstPortParametersSetter, secondPortParametersSetter);
        }

        [Fact]
        public void CheckNoPublicConstructors()
        {
            // Arrange
            Type portParametersSetter = typeof(PortParametersSetter);
            ConstructorInfo[] constructorsInfoArray = portParametersSetter.GetConstructors();
            bool hasPublicConstructors = false;

            // Act
            foreach (ConstructorInfo constructorInfo in constructorsInfoArray)
            {
                if (constructorInfo.IsPublic)
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
