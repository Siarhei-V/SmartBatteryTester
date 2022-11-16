using AutoMapper;
using SmartBatteryTesterWebApp.BLL.DTO;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.DAL.Entities;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;

namespace SmartBatteryTesterWebApp.BLL.Services
{
    public class MeasurementInputService : IMeasurementInputService
    {
        IMeasurementSetRepository _measurementSetRepository;

        public MeasurementInputService(IMeasurementSetRepository measurementSetRepository)
        {
            _measurementSetRepository = measurementSetRepository;
        }

        public void MakeMeasurement(MeasurementDTO measurement)
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
