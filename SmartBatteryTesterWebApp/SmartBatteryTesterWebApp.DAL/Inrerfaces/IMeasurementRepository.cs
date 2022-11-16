using SmartBatteryTesterWebApp.DAL.Entities;

namespace SmartBatteryTesterWebApp.DAL.Inrerfaces
{
    public interface IMeasurementRepository
    {
        IQueryable<Measurement> Measurements { get; }
        void Create(Measurement measurement);
    }
}
