using SmartBatteryTesterWebApp.DAL.Entities;

namespace SmartBatteryTesterWebApp.DAL.Inrerfaces
{
    public interface IMeasurementSetRepository
    {
        public Task<List<MeasurementSet>> GetMeasurementSetsAsync();
        Task CreateAsync(MeasurementSet measurementSet);
        Task UpdateAsync(MeasurementSet measurementSet);
        Task<MeasurementSet> FindAsync(string str);
    }
}
