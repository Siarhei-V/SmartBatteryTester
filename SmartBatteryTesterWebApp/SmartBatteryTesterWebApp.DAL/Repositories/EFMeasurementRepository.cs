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

        public IQueryable<Measurement> Measurements => throw new NotImplementedException();

        public void Create(Measurement measurement)
        {
            _applicationContext.Add(measurement);
            _applicationContext.SaveChanges();
        }
    }
}
