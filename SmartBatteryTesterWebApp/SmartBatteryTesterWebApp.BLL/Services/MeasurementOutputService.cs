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

        public List<MeasurementSetDTO> GetMeasurementSet()
        {
            var measurementSets = _measurementSetRepository.GetMeasurementSets();
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSet, MeasurementSetDTO>()).CreateMapper();
            var measurementSetDtos = mapper.Map<List<MeasurementSet>, List<MeasurementSetDTO>>(measurementSets);
            return measurementSetDtos;
        }

        public List<MeasurementDTO> GetMeasurement(int measurementSetId)
        {
            var measurements = _measurementRepository.GetMeasurements(measurementSetId);
            var mapper = new MapperConfiguration(m => m.CreateMap<Measurement, MeasurementDTO>()).CreateMapper();
            var measurementDtos = mapper.Map<List<Measurement>, List<MeasurementDTO>>(measurements);
            return measurementDtos;
        }

        public MeasurementSetDTO FindMeasurementSet(string measurementSetStatus)
        {
            var measurementSet = _measurementSetRepository.Find(measurementSetStatus);
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSet, MeasurementSetDTO>()).CreateMapper();
            var measurementSetDtos = mapper.Map<MeasurementSet, MeasurementSetDTO>(measurementSet);
            return measurementSetDtos;
        }
    }
}
