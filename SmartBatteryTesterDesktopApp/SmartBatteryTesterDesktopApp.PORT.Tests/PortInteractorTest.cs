using Moq;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using System.Reflection;
using Xunit;

namespace SmartBatteryTesterDesktopApp.PORT.Tests
{

    public class PortInteractorTest
    {
        IUiInteractorInputPort _uiInteractorInputPort;
        IUsartInteractorInputPort _usartInteractorInputPort;
        Mock<IDataGetter> _dataGetterMock;
        Mock<IPortController> _portControllerMock;

        public PortInteractorTest()
        {
            _uiInteractorInputPort = PortInteractor.Instance;
            _usartInteractorInputPort = PortInteractor.Instance;
            _dataGetterMock = new Mock<IDataGetter>();
            _portControllerMock = new Mock<IPortController>();
        }


        [Fact]
        public void CheckSingletonInstance()
        {
            // Arrange

            // Act
            var firstPortInteractor = PortInteractor.Instance;
            var secondPortInteractor = PortInteractor.Instance;

            // Assert
            Assert.Equal(firstPortInteractor, secondPortInteractor);
        }

        [Fact]
        public void CheckNoPublicConstructors()
        {
            // Arrange
            Type interactorType = typeof(PortInteractor);
            bool hasPublicConstructors = false;

            // Act
            ConstructorInfo[] constructorInfo = interactorType.GetConstructors();
            foreach (var item in constructorInfo)
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

        [Fact]
        public void SendUsartData_CheckGetDataCollingWhenDataNotValid()
        {
            // Arrange
            _uiInteractorInputPort.DataSender = _dataGetterMock.Object;

            // Act
            _usartInteractorInputPort.SendUsartData("");

            // Assert
            _dataGetterMock.Verify(m => m.GetData(""), Times.Once());
        }

        [Fact]
        public void StartDischarging_CheckStartDischargingColling()
        {
            // Arrange
            _usartInteractorInputPort.PortController = _portControllerMock.Object;

            // Act
            _uiInteractorInputPort.StartDischarging(new Dictionary<string, string>());

            // Assert
            _portControllerMock.Verify(m => m.StartDischarging(It.IsAny<Dictionary<string, string>>()), Times.Once());
        }

        [Fact]
        public void StartDischarging_CheckStoptDischargingColling()
        {
            // Arrange
            _usartInteractorInputPort.PortController = _portControllerMock.Object;

            // Act
            _uiInteractorInputPort.StopDischarging();

            // Assert
            _portControllerMock.Verify(m => m.StopDischarging(), Times.Once());
        }
    }
}
