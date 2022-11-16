using AutoMapper;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.DAL.Entities;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;

namespace SmartBatteryTesterWebApp.BLL.Services
{
    public class MeasurementService : IMeasurementService
    {
        readonly IMeasurementRepository _measurementRepository;
        readonly IMeasurementSetRepository _measurementSetRepository;

        public MeasurementService(IMeasurementRepository measurementRepository, IMeasurementSetRepository measurementSetRepository)
        {
            _measurementRepository = measurementRepository;
            _measurementSetRepository = measurementSetRepository;
        }

        public List<MeasurementSetDTO> GetMeasurementSet()
        {
            var measurementSets = _measurementSetRepository.GetMeasurementSets();
            var mapper = new MapperConfiguration(m => m.CreateMap<MeasurementSet, MeasurementSetDTO>()).CreateMapper();
            var MeasurementSetDtos = mapper.Map<IQueryable<MeasurementSet>, List<MeasurementSetDTO>>(measurementSets);
            return MeasurementSetDtos;
        }

        public void MakeMeasurement(MeasurementDTO measurementDTO)
        {
            throw new NotImplementedException();
        }

        public void MakeMeasurementSet(MeasurementSetDTO measurementSetDto)
        {
            var config = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSet>());
            var mapper = new Mapper(config);
            MeasurementSet measurementSet = mapper.Map<MeasurementSetDTO, MeasurementSet>(measurementSetDto);
            _measurementSetRepository.Create(measurementSet);
        }

    }
}
