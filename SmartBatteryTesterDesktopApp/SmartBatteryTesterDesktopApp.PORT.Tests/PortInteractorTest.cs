using Moq;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Models;
using System.Reflection;
using Xunit;

namespace SmartBatteryTesterDesktopApp.PORT.Tests
{

    public class PortInteractorTest
    {
        IUsartInteractorInputPort _usartInteractorPort;
        IUiInteractorInputPort _uiInteractorPort;
        IDaInteractorInputPort _daInteractorPort;
        Mock<IDataGetter> _dataGetterMock;
        Mock<IDataSaver> _dataSaverMock;
        Mock<IPortController> _portControllerMock;

        public PortInteractorTest()
        {
            _usartInteractorPort = PortInteractor.Instance;
            _uiInteractorPort = PortInteractor.Instance;
            _daInteractorPort = PortInteractor.Instance;

            _dataGetterMock = new Mock<IDataGetter>();
            _dataSaverMock = new Mock<IDataSaver>();
            _portControllerMock = new Mock<IPortController>();

            _usartInteractorPort.PortController = _portControllerMock.Object;
            _daInteractorPort.DataSaver = _dataSaverMock.Object;
            _uiInteractorPort.DataSender = _dataGetterMock.Object;
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
            bool hasNotPrivateConstructors = false;

            // Act
            ConstructorInfo[] constructorInfo = interactorType.GetConstructors();
            foreach (var item in constructorInfo)
            {
                if (!item.IsPrivate)
                {
                    hasNotPrivateConstructors = true;
                    break;
                }
            }

            // Assert
            Assert.False(hasNotPrivateConstructors);

        }

        [Fact]
        public void ConnectToPort_CheckPortControllerColling()
        {
            // Arrange
            var parameters = new Dictionary<string, string>() { ["TempDischargingCurrent"] = "1" };

            // Act
            _uiInteractorPort.ConnectToPort(parameters);

            // Assert
            _portControllerMock.Verify(m => m.OpenPort(parameters), Times.Once);
        }

        [Fact]
        public void StartDischarging_CheckStartDischargingMethodColling()
        {
            // Arrange

            // Act
            _uiInteractorPort.StartDischarging(It.IsAny<bool>());

            // Assert
            _portControllerMock.Verify(m => m.StartDischarging(), Times.Once);
        }

        [Fact]
        public void SendUsartData_CheckDataFromUsartHandling()
        {
            // Arrange
            _uiInteractorPort.SetDischargingParams("0", "1", "0");

            // Act
            // Assert
            _uiInteractorPort.StartDischarging(false);
            _uiInteractorPort.ConnectToPort(new Dictionary<string, string> { ["TempDischargingCurrent"] = "1" });
            _usartInteractorPort.SendUsartData("3");
            _dataSaverMock.Verify(m => m.TransmitData(It.IsAny<MeasurementModel>()), Times.Never);
            _dataGetterMock.Verify(m => m.GetData(It.IsAny<string>()), Times.Once);
            
            _uiInteractorPort.StartDischarging(true);
            _usartInteractorPort.SendUsartData("2");
            _dataSaverMock.Verify(m => m.TransmitData(It.IsAny<MeasurementModel>()), Times.Once);
            _dataGetterMock.Verify(m => m.GetData(It.IsAny<string>()), Times.Exactly(2));

            _uiInteractorPort.ConnectToPort(new Dictionary<string, string> { ["TempDischargingCurrent"] = "1" });
            _usartInteractorPort.SendUsartData("2");
            _dataSaverMock.Verify(m => m.TransmitData(It.IsAny<MeasurementModel>()), Times.Once);
            _dataGetterMock.Verify(m => m.GetData(It.IsAny<string>()), Times.Exactly(3));

            _usartInteractorPort.SendUsartData("1");
            _dataSaverMock.Verify(m => m.TransmitData(It.IsAny<MeasurementModel>()), Times.Exactly(2));
            _dataGetterMock.Verify(m => m.GetData(It.IsAny<string>()), Times.Exactly(4));
        }

        [Fact]
        public void SendUsartData_CheckDataSavingException()
        {
            // Arrange
            _dataSaverMock.Setup(m => m.TransmitData(It.IsAny<MeasurementModel>())).Throws<InvalidOperationException>();

            // Act
            _uiInteractorPort.StartDischarging(true);
            _usartInteractorPort.SendUsartData("1");

            // Assert
            _dataGetterMock.Verify(m => m.GetWebStatus("Ошибка подключения к веб"), Times.Once);

        }

        [Fact]
        public void SendUsartData_CheckDischargingFinishInOnlineMode()
        {
            // Arrange

            // Act
            _uiInteractorPort.SetDischargingParams("1", "1", "0");
            _uiInteractorPort.StartDischarging(true);

            // Assert
            _usartInteractorPort.SendUsartData("2");
            _dataSaverMock.Verify(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), It.IsAny<decimal>(), "Батарея разряжена"),
                Times.Never);
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Never);
            _dataGetterMock.Verify(m => m.GetPortStatus("Порт закрыт"), Times.Never);
            _portControllerMock.Verify(m => m.StopDischarging(), Times.Never);

            _usartInteractorPort.SendUsartData("1");
            _dataSaverMock.Verify(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), It.IsAny<decimal>(), "Батарея разряжена"),
                Times.Once);
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Once);
            _dataGetterMock.Verify(m => m.GetPortStatus("Порт закрыт"), Times.Once);
            _portControllerMock.Verify(m => m.StopDischarging(), Times.Once);
        }

        [Fact]
        public void SendUsartData_CheckDischargingFinishInOfflineMode()
        {
            // Arrange

            // Act
            _uiInteractorPort.SetDischargingParams("1", "1", "0");
            _uiInteractorPort.StartDischarging(false);

            // Assert
            _usartInteractorPort.SendUsartData("2");
            _dataSaverMock.Verify(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), It.IsAny<decimal>(), "Батарея разряжена"),
                Times.Never);
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Never);
            _dataGetterMock.Verify(m => m.GetPortStatus("Порт закрыт"), Times.Never);
            _portControllerMock.Verify(m => m.StopDischarging(), Times.Never);

            _usartInteractorPort.SendUsartData("1");
            _dataSaverMock.Verify(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), It.IsAny<decimal>(), "Батарея разряжена"),
                Times.Never);
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Never);
            _dataGetterMock.Verify(m => m.GetPortStatus("Порт закрыт"), Times.Once);
            _portControllerMock.Verify(m => m.StopDischarging(), Times.Once);
        }

        [Fact]
        public void SendUsartData_CheckExceptionOfDischargingFinishInOnlineMode()
        {
            // Arrange
            _dataSaverMock.Setup(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), It.IsAny<decimal>(), "Батарея разряжена"))
                .Throws(new Exception());

            // Act
            _uiInteractorPort.SetDischargingParams("1", "1", "0");
            _uiInteractorPort.StartDischarging(true);
            _usartInteractorPort.SendUsartData("2");
            _usartInteractorPort.SendUsartData("1");

            // Assert
            _dataGetterMock.Verify(m => m.GetWebStatus("Ошибка подключения к веб"), Times.Once);
        }

        [Fact]
        public void StopDischarging_CheckStoptDischargingInOfflineMode()
        {
            // Arrange

            // Act
            _uiInteractorPort.StartDischarging(false);
            _uiInteractorPort.StopDischarging();

            // Assert
            _portControllerMock.Verify(m => m.StopDischarging(), Times.Once());
            _dataSaverMock.Verify(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), 0, "Разряд батареи прерван"), Times.Never);
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Never);
        }

        [Fact]
        public void StopDischarging_CheckStoptDischargingInOnlineMode()
        {
            // Arrange

            // Act
            _uiInteractorPort.StartDischarging(true);
            _uiInteractorPort.StopDischarging();

            // Assert
            _dataSaverMock.Verify(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), 0, "Разряд батареи прерван"), Times.Once);
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Once);
        }

        [Fact]
        public void StopDischarging_CheckDataSaverException()
        {
            // Arrange
            _dataSaverMock.Setup(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), 0, "Разряд батареи прерван")).Throws<Exception>();

            // Act
            _uiInteractorPort.StartDischarging(true);
            _uiInteractorPort.StopDischarging();

            // Assert
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Once);
        }

        [Fact]
        public void DisconnectDevice_CheckPortControllerColling()
        {
            // Arrange

            // Act
            _uiInteractorPort.DisconnectDevice();

            // Assert
            _portControllerMock.Verify(m => m.ClosePort(), Times.Once);
        }

        [Fact]
        public void DisconnectWeb_CheckDataGetterCollingInOfflineMode()
        {
            // Arrange

            // Act
            _uiInteractorPort.DisconnectWeb();

            // Assert
            _dataSaverMock.Verify(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), 0, "Разряд батареи не был запущен"), Times.Never);
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Once);
        }

        [Fact]
        public void DisconnectWeb_CheckDataGetterCollingInOnlineMode()
        {
            // Arrange

            // Act
            _uiInteractorPort.CreateNewTest("");
            _uiInteractorPort.DisconnectWeb();

            // Assert
            _dataSaverMock.Verify(m => m.FinishDataTransfer(It.IsAny<TimeSpan>(), 0, "Разряд батареи не был запущен"), Times.Once);
            _dataGetterMock.Verify(m => m.GetWebStatus("Соединение с веб остановлено"), Times.Once);
        }

        [Fact]
        public void CreateNewTest_CheckMethodsColling()
        {
            // Arrange

            // Act
            _uiInteractorPort.CreateNewTest("test");

            // Assert
            _dataGetterMock.Verify(m => m.GetWebStatus("Попытка подключения к веб"), Times.Once);
            _dataSaverMock.Verify(m => m.StartDataTransfer("test"), Times.Once);
            _dataGetterMock.Verify(m => m.GetWebStatus("Связь установлена"), Times.Once);
        }

        [Fact]
        public void CreateNewTest_CheckDataSaverException()
        {
            // Arrange
            _dataSaverMock.Setup(m => m.StartDataTransfer("test")).Throws<Exception>();

            // Act
            _uiInteractorPort.CreateNewTest("test");

            // Assert
            _dataGetterMock.Verify(m => m.GetWebStatus("Попытка подключения к веб"), Times.Once);
            _dataSaverMock.Verify(m => m.StartDataTransfer("test"), Times.Once);
            _dataGetterMock.Verify(m => m.GetWebStatus("Ошибка подключения к веб"), Times.Once);
            _dataGetterMock.Verify(m => m.GetWebStatus("Связь установлена"), Times.Never);
        }
    }
}
