using SmartBatteryTesterWebApp.DAL.Entities;

namespace SmartBatteryTesterWebApp.DAL.Inrerfaces
{
    public interface IMeasurementRepository
    {
        public List<Measurement> GetMeasurements(int measurementSetId);
        void Create(Measurement measurement);
    }
}
