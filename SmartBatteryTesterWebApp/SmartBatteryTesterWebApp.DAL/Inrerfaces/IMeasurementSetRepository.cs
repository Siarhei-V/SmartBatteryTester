using SmartBatteryTesterWebApp.DAL.Entities;

namespace SmartBatteryTesterWebApp.DAL.Inrerfaces
{
    public interface IMeasurementSetRepository
    {
        List<MeasurementSet> GetMeasurementSets();
        void Create(MeasurementSet measurementSet);
        void Update(MeasurementSet measurementSet);
        MeasurementSet Find(string str);
    }
}
