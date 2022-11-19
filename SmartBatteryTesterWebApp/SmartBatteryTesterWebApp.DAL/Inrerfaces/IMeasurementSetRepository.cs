using SmartBatteryTesterWebApp.DAL.Entities;

namespace SmartBatteryTesterWebApp.DAL.Inrerfaces
{
    public interface IMeasurementSetRepository
    {
        public IQueryable<MeasurementSet> GetMeasurementSets();
        void Create(MeasurementSet measurementSet);
        public void Update(MeasurementSet measurementSet);
        public MeasurementSet Find(string str);
    }
}
