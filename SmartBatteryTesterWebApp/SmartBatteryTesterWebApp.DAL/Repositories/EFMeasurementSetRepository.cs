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

        public List<MeasurementSet> GetMeasurementSets() 
        {
            return _applicationContext.MeasurementSets.ToList();
        }

        public void Create(MeasurementSet measurementSet)
        {
            _applicationContext.Add(measurementSet);
            _applicationContext.SaveChanges();
        }

        public void Update(MeasurementSet measurementSet)
        {
            _applicationContext.MeasurementSets.Update(measurementSet);
            _applicationContext.SaveChanges();
        }

        public MeasurementSet Find(string str)
        {
            var res = _applicationContext.MeasurementSets.Where(m => m.MeasurementStatus == str).ToList();
            return res.LastOrDefault();
        }
    }
}
