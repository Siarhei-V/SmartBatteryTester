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
            var config = new MapperConfiguration(m => m.CreateMap<MeasurementDTO, Measurement>());
            var mapper = new Mapper(config);
            Measurement measurement = mapper.Map<MeasurementDTO, Measurement>(measurementDto);
            await _measurementRepository.CreateAsync(measurement);
        }

        public async Task MakeMeasurementSetAsync(MeasurementSetDTO measurementSetDto)
        {
            var config = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSet>());
            var mapper = new Mapper(config);
            MeasurementSet measurementSet = mapper.Map<MeasurementSetDTO, MeasurementSet>(measurementSetDto);
            await _measurementSetRepository.CreateAsync(measurementSet);
        }

        public async Task UpdateMeasurementSetAsync(MeasurementSetDTO measurementSetDto)
        {
            var config = new MapperConfiguration(m => m.CreateMap<MeasurementSetDTO, MeasurementSet>());
            var mapper = new Mapper(config);
            MeasurementSet measurementSet = mapper.Map<MeasurementSetDTO, MeasurementSet>(measurementSetDto);
            await _measurementSetRepository.UpdateAsync(measurementSet);
        }
    }
}
