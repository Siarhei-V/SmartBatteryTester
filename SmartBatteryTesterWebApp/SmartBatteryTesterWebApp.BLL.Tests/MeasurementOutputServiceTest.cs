﻿using Moq;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.BLL.Services;
using SmartBatteryTesterWebApp.DAL.Entities;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SmartBatteryTesterWebApp.BLL.Tests
{
    public class MeasurementOutputServiceTest
    {
        Mock<IMeasurementRepository> _measurementRepositoryMock;
        Mock<IMeasurementSetRepository> _measurementSetRepositoryMock;
        IMeasurementOutputService _measurementOutputService;
        List<MeasurementSet> _testMeasurementSets;

        public MeasurementOutputServiceTest()
        {
            _measurementRepositoryMock = new Mock<IMeasurementRepository>();
            _measurementSetRepositoryMock = new Mock<IMeasurementSetRepository>();
            _measurementOutputService = new MeasurementOutputService(_measurementRepositoryMock.Object,
                _measurementSetRepositoryMock.Object);

            _testMeasurementSets = new List<MeasurementSet>()
            {
                new MeasurementSet() { Id = 1, MeasurementName = "Test measurement 1", MeasurementStatus = "Test status 1"},
                new MeasurementSet() { Id = 2, MeasurementName = "Test measurement 2", MeasurementStatus = "Test status 2"},
            };
        }

        #region GetMeasurementSet Tests
        [Fact]
        public void GetMeasurementSet_CheckGetMeasurementSetsMethodColling()
        {
            // Arrange

            // Act
            _measurementOutputService.GetMeasurementSet();

            // Assert
            _measurementSetRepositoryMock.Verify(m => m.GetMeasurementSets(), Times.Once());
        }

        [Fact]
        public void GetMeasurementSet_CheckReturnedValues()
        {
            // Arrange
            _measurementSetRepositoryMock.Setup(m => m.GetMeasurementSets()).Returns(_testMeasurementSets);

            // Act
            var methodResult = _measurementOutputService.GetMeasurementSet();

            // Assert
            var typeResult = Assert.IsType<List<MeasurementSetDTO>>(methodResult);
            var result = Assert.IsAssignableFrom<List<MeasurementSetDTO>>(typeResult);
            Assert.Equal(2, result.Count);

            for (int i = 0; i < _testMeasurementSets.Count; i++)
            {
                Assert.Equal(_testMeasurementSets[i].Id, result[i].Id);
                Assert.Equal(_testMeasurementSets[i].MeasurementName, result[i].MeasurementName);
                Assert.Equal(_testMeasurementSets[i].MeasurementStatus, result[i].MeasurementStatus);
            }
        }
        #endregion

        #region GetMeasurement Tests
        [Fact]
        public void GetMeasurement_CheckGetMeasurementSetsMethodColling()
        {
            // Arrange

            // Act
            _measurementOutputService.GetMeasurement(It.IsAny<int>());

            // Assert
            _measurementRepositoryMock.Verify(m => m.GetMeasurements(It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 0)]
        public void GetMeasurement_CheckReturnedValues(int testCase, int testResultCount)
        {
            // Arrange
            List<Measurement> testMeasurements = new List<Measurement>();

            switch (testCase)
            {
                case 0:
                    testMeasurements = new List<Measurement>()
                    {
                        new Measurement() { Id = 1, Voltage = 10, Current = 5, 
                            MeasurementDateTime = new DateTime(2022, 11, 20, 18, 53, 10).ToString(), 
                            MeasurementSetId = _testMeasurementSets[0].Id, 
                            SetOfMeasurements = _testMeasurementSets[0]},

                        new Measurement() { Id = 2, Voltage = 4, Current = 3,
                            MeasurementDateTime = new DateTime(2022, 11, 21, 18, 50, 00).ToString(), 
                            MeasurementSetId = _testMeasurementSets[0].Id,
                            SetOfMeasurements = _testMeasurementSets[0]},
                    };
                    break;
                case 1:
                    testMeasurements = new List<Measurement>()
                    {
                        new Measurement() { Id = 5, Voltage = 1, Current = 2,
                            MeasurementDateTime = new DateTime(1111, 11, 11, 11, 11, 11).ToString(),
                            MeasurementSetId = _testMeasurementSets[1].Id,
                            SetOfMeasurements = _testMeasurementSets[1]},

                        new Measurement() { Id = 6, Voltage = 2, Current = 3,
                            MeasurementDateTime = new DateTime(2222, 12, 22, 22, 22, 22).ToString(),
                            MeasurementSetId = _testMeasurementSets[1].Id,
                            SetOfMeasurements = _testMeasurementSets[1]},

                        new Measurement() { Id = 11, Voltage = 12, Current = 13,
                            MeasurementDateTime = new DateTime(3333, 3, 30, 3, 3, 3).ToString(),
                            MeasurementSetId = _testMeasurementSets[1].Id,
                            SetOfMeasurements = _testMeasurementSets[1]},
                    };
                    break;
            }

            _measurementRepositoryMock.Setup(m => m.GetMeasurements(testCase)).Returns(testMeasurements);

            // Act
            var methodResult = _measurementOutputService.GetMeasurement(testCase);

            // Assert
            var typeResult = Assert.IsType<List<MeasurementDTO>>(methodResult);
            var result = Assert.IsAssignableFrom<List<MeasurementDTO>>(typeResult);
            Assert.Equal(testResultCount, result.Count);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(testMeasurements[i].Id, result[i].Id);
                Assert.Equal(testMeasurements[i].Voltage, result[i].Voltage);
                Assert.Equal(testMeasurements[i].Current, result[i].Current);
                Assert.Equal(testMeasurements[i].MeasurementDateTime, result[i].MeasurementDateTime);
                Assert.Equal(testMeasurements[i].MeasurementSetId, result[i].MeasurementSetId);
            }
        }
        #endregion

        #region FindMeasurementSet Tests
        [Fact]
        public void FindMeasurementSet_CheckFindMethodColling()
        {
            // Arrange

            // Act
            _measurementOutputService.FindMeasurementSet(It.IsAny<string>());

            // Assert
            _measurementSetRepositoryMock.Verify(m => m.Find(It.IsAny<string>()), Times.Once());
        }

        [Theory]
        [InlineData("Test status 1", 0)]
        [InlineData("Test status 2", 1)]
        public void FindMeasurementSet_CheckReturnedValues(string status, int index)
        {
            // Arrange
            _measurementSetRepositoryMock.Setup(m => m.Find(status)).Returns(_testMeasurementSets[index]);

            // Act
            var methodResult = _measurementOutputService.FindMeasurementSet(status);

            // Assert
            var typeResult = Assert.IsType<MeasurementSetDTO>(methodResult);
            var result = Assert.IsAssignableFrom<MeasurementSetDTO>(typeResult);

            Assert.Equal(_testMeasurementSets[index].Id, result.Id);
            Assert.Equal(_testMeasurementSets[index].MeasurementName, result.MeasurementName);
            Assert.Equal(_testMeasurementSets[index].MeasurementStatus, result.MeasurementStatus);
        }
        #endregion
    }
}
