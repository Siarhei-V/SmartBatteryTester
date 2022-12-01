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

        public List<Measurement> GetMeasurements(int measurementSetId)
        {
            return _applicationContext.Measurements.Where(m => m.MeasurementSetId == measurementSetId).ToList();
        }

        public void Create(Measurement measurement)
        {
            _applicationContext.Add(measurement);
            _applicationContext.SaveChanges();
        }

    }
}
