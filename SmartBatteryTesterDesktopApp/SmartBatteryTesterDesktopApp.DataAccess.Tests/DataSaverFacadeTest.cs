using Moq;
using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;
using SmartBatteryTesterDesktopApp.DataAccess.Models;
using SmartBatteryTesterDesktopApp.PORT.Interfaces;
using SmartBatteryTesterDesktopApp.PORT.Models;
using System;
using Xunit;

namespace SmartBatteryTesterDesktopApp.DataAccess.Tests
{
    public class DataSaverFacadeTest
    {
        IDataSaver _dataSaver;
        Mock<IDataSenderFactory> _dataSenderFactoryMock;
        Mock<IDataSender> _dataToWebApiSenderMock;
        Mock<IDataSender> _dataToSignalRSenderMock;
        TestDataModel _dataModel;

        public DataSaverFacadeTest()
        {
            _dataModel = new TestDataModel();
            _dataSenderFactoryMock = new Mock<IDataSenderFactory>();
            _dataToSignalRSenderMock = new Mock<IDataSender>();
            _dataToWebApiSenderMock = new Mock<IDataSender>();

            _dataSenderFactoryMock.Setup(m => m.MakeDataToWebApiSender()).Returns(_dataToWebApiSenderMock.Object);
            _dataSenderFactoryMock.Setup(m => m.MakeDataToSignalRSender()).Returns(_dataToSignalRSenderMock.Object);

            _dataSaver = new DataSaverFacade(_dataSenderFactoryMock.Object, _dataModel);
        }

        [Fact]
        public void StartDataTransfer_CheckSendersColling()
        {
            // Arrange
            string testName = "first test";

            // Act
            _dataSaver.StartDataTransfer(testName);

            // Assert
            Assert.Equal(testName, _dataModel.MeasurementName);
            _dataToWebApiSenderMock.Verify(m => m.StartDataTransfer(_dataModel), Times.Once);
            _dataToSignalRSenderMock.Verify(m => m.StartDataTransfer(_dataModel), Times.Once);
        }

        [Fact]
        public void TransmitData_CheckSendersColling()
        {
            // Arrange
            MeasurementModel measurementModel = new MeasurementModel();

            // Act
            _dataSaver.TransmitData(measurementModel);

            // Assert
            _dataToWebApiSenderMock.Verify(m => m.TransmitData(It.IsAny<MeasurementDataModel>()), Times.Once);
            _dataToSignalRSenderMock.Verify(m => m.TransmitData(It.IsAny<MeasurementDataModel>()), Times.Once);
        }

        [Fact]
        public void FinishDataTransfer_CheckSendersColling()
        {
            // Arrange
            var duration = new TimeSpan(5, 4, 3);
            decimal capacity = 100;
            string status = "krevetka";

            // Act
            _dataSaver.FinishDataTransfer(duration, capacity, status);

            // Assert
            Assert.Equal(duration, _dataModel.DischargeDuration);
            Assert.Equal(capacity, _dataModel.ResultCapacity);
            Assert.Equal(status, _dataModel.MeasurementStatus);

            _dataToWebApiSenderMock.Verify(m => m.FinishDataTransfer(), Times.Once);
            _dataToSignalRSenderMock.Verify(m => m.FinishDataTransfer(), Times.Once);
        }
    }
}