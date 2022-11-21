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

        public async Task MakeMeasurementAsync(MeasurementDTO measurementDto)
        {
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementDTO, Measurement>()).CreateMapper();
            Measurement measurement = mapper.Map<MeasurementDTO, Measurement>(measurementDto);
            await _measurementRepository.CreateAsync(measurement);
        }

        public async Task MakeMeasurementSetAsync(MeasurementSetDTO measurementSetDto)
        {
            var measurementSet = MapMeasurementSetDtoToMeasurementSet(measurementSetDto);
            await _measurementSetRepository.CreateAsync(measurementSet);
        }

        public async Task UpdateMeasurementSetAsync(MeasurementSetDTO measurementSetDto)
        {
            var measurementSet = MapMeasurementSetDtoToMeasurementSet(measurementSetDto);
            await _measurementSetRepository.UpdateAsync(measurementSet);
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
