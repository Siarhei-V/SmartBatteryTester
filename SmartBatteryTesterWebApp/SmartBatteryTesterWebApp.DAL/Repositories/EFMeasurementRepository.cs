using SmartBatteryTesterWebApp.DAL.Entities;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;

namespace SmartBatteryTesterWebApp.DAL.Repositories
{
    public class EFMeasurementRepository : IMeasurementRepository
    {
        public IQueryable<Measurement> Measurements => throw new NotImplementedException();

        public void Create(Measurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
