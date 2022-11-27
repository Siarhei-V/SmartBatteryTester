using Microsoft.EntityFrameworkCore;
using SmartBatteryTesterWebApp.DAL.EF;
using SmartBatteryTesterWebApp.DAL.Entities;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;

namespace SmartBatteryTesterWebApp.DAL.Repositories
{
    public class EFMeasurementSetRepository : IMeasurementSetRepository
    {
        ApplicationContext _applicationContext;

        public EFMeasurementSetRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<MeasurementSet>> GetMeasurementSetsAsync() 
        {
            return await _applicationContext.MeasurementSets.ToListAsync();
        }

        public async Task CreateAsync(MeasurementSet measurementSet)
        {
            _applicationContext.Add(measurementSet);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MeasurementSet measurementSet)
        {
            _applicationContext.MeasurementSets.Update(measurementSet);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<MeasurementSet> FindAsync(string str)
        {
            var res = await _applicationContext.MeasurementSets.Where(m => m.MeasurementStatus == str).ToListAsync();
            return res.LastOrDefault();
        }
    }
}
