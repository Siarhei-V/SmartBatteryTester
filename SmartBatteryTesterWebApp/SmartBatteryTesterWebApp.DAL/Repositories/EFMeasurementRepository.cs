using Microsoft.EntityFrameworkCore;
using SmartBatteryTesterWebApp.DAL.EF;
using SmartBatteryTesterWebApp.DAL.Entities;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;

namespace SmartBatteryTesterWebApp.DAL.Repositories
{
    public class EFMeasurementRepository : IMeasurementRepository
    {
        ApplicationContext _applicationContext;

        public EFMeasurementRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Measurement>> GetMeasurementsAsync(int measurementSetId)
        {
            return await _applicationContext.Measurements.Where(m => m.MeasurementSetId == measurementSetId).ToListAsync();
        }

        public async Task CreateAsync(Measurement measurement)
        {
            _applicationContext.Add(measurement);
            await _applicationContext.SaveChangesAsync();
        }

    }
}
