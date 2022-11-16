﻿using SmartBatteryTesterWebApp.DAL.EF;
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

        public IQueryable<MeasurementSet> GetMeasurementSets() 
        {
            return _applicationContext.MeasurementSets;
        }

        public void Create(MeasurementSet measurementSet)
        {
            _applicationContext.Add(measurementSet);
            _applicationContext.SaveChanges();
        }
    }
}