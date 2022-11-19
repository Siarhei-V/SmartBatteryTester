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

        public IQueryable<Measurement> GetMeasurements(int measurementSetId)
        {
            return _applicationContext.Measurements.Where(m => m.MeasurementSetId == measurementSetId);
        }

        public void Create(Measurement measurement)
        {
            _applicationContext.Add(measurement);
            _applicationContext.SaveChanges();
        }

    }
}
