using SmartBatteryTesterDesktopApp.DataAccess.Interfaces;
using Xunit;

namespace SmartBatteryTesterDesktopApp.DataAccess.Tests
{
    public class DataSenderFactoryTest
    {
        IDataSenderFactory _factory;

        public DataSenderFactoryTest()
        {
            _factory = new DataSenderFactory();
        }

        [Fact]
        public void MakeDataToSignalRSender_CheckReturnedType()
        {
            // Arrange

            // Act
            var result = _factory.MakeDataToSignalRSender();

            // Assert
            Assert.IsType<DataToSignalRSender>(result);
        }

        [Fact]
        public void MakeDataToWebApiSender_CheckReturnedType()
        {
            // Arrange

            // Act
            var result = _factory.MakeDataToWebApiSender();

            // Assert
            Assert.IsType<DataToWebApiSender>(result);
        }
    }
}
