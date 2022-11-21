using AutoMapper;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.DAL.Entities;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;

namespace SmartBatteryTesterWebApp.BLL.Services
{
    public class MeasurementOutputService : IMeasurementOutputService
    {
        readonly IMeasurementRepository _measurementRepository;
        readonly IMeasurementSetRepository _measurementSetRepository;

        public MeasurementOutputService(IMeasurementRepository measurementRepository, IMeasurementSetRepository measurementSetRepository)
        {
            _measurementRepository = measurementRepository;
            _measurementSetRepository = measurementSetRepository;
        }

        public async Task<List<MeasurementSetDTO>> GetMeasurementSetAsync()
        {
            var measurementSets = await _measurementSetRepository.GetMeasurementSetsAsync();
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSet, MeasurementSetDTO>()).CreateMapper();
            var measurementSetDtos = mapper.Map<List<MeasurementSet>, List<MeasurementSetDTO>>(measurementSets);
            return measurementSetDtos;
        }

        public async Task<List<MeasurementDTO>> GetMeasurementAsync(int measurementSetId)
        {
            var measurements = await _measurementRepository.GetMeasurementsAsync(measurementSetId);
            var mapper = new MapperConfiguration(m => m.CreateMap<Measurement, MeasurementDTO>()).CreateMapper();
            var measurementDtos = mapper.Map<List<Measurement>, List<MeasurementDTO>>(measurements);
            return measurementDtos;
        }

        public async Task<MeasurementSetDTO> FindMeasurementSetAsync(string measurementSetStatus)
        {
            var measurementSet = await _measurementSetRepository.FindAsync(measurementSetStatus);
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSet, MeasurementSetDTO>()).CreateMapper();
            var measurementSetDtos = mapper.Map<MeasurementSet, MeasurementSetDTO>(measurementSet);
            return measurementSetDtos;
        }
    }
}
