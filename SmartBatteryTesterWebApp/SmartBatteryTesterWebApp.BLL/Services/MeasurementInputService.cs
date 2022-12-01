using AutoMapper;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.DAL.Entities;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;

namespace SmartBatteryTesterWebApp.BLL.Services
{
    public class MeasurementInputService : IMeasurementInputService
    {
        readonly IMeasurementRepository _measurementRepository;
        readonly IMeasurementSetRepository _measurementSetRepository;

        public MeasurementInputService(IMeasurementRepository measurementRepository, 
            IMeasurementSetRepository measurementSetRepository)
        {
            _measurementRepository = measurementRepository;
            _measurementSetRepository = measurementSetRepository;
        }

        public void MakeMeasurement(MeasurementDTO measurementDto)
        {
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementDTO, Measurement>()).CreateMapper();
            Measurement measurement = mapper.Map<MeasurementDTO, Measurement>(measurementDto);
            _measurementRepository.Create(measurement);
        }

        public void MakeMeasurementSet(MeasurementSetDTO measurementSetDto)
        {
            var measurementSet = MapMeasurementSetDtoToMeasurementSet(measurementSetDto);
            _measurementSetRepository.Create(measurementSet);
        }

        public void UpdateMeasurementSet(MeasurementSetDTO measurementSetDto)
        {
            var measurementSet = MapMeasurementSetDtoToMeasurementSet(measurementSetDto);
            _measurementSetRepository.Update(measurementSet);
        }

        #region Private Methods
        private MeasurementSet MapMeasurementSetDtoToMeasurementSet(MeasurementSetDTO measurementSetDTO)
        {
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSet>()).CreateMapper();
            return mapper.Map<MeasurementSetDTO, MeasurementSet>(measurementSetDTO);
        }
        #endregion
    }
}
