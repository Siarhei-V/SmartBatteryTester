using SmartBatteryTesterWebApp.DAL.Entities;

namespace SmartBatteryTesterWebApp.DAL.Inrerfaces
{
    public interface IMeasurementRepository
    {
        public Task<List<Measurement>> GetMeasurementsAsync(int measurementSetId);
        Task CreateAsync(Measurement measurement);
    }
}
