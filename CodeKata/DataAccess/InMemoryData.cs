using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeKata.DataAccess.Interface;
using CodeKata.Entities;

namespace CodeKata.DataAccess
{
    class InMemoryData : IDriverRepository
    {
        readonly List<Trip> tripList;

        readonly List<Driver> driverList; 

        public InMemoryData()
        {
            driverList = new List<Driver>();
            tripList = new List<Trip>();
        }

        public void RegisterDriver(Driver driver)
        {driverList.Add(driver);
        }

        public void RegisterTrip(Trip trip)
        {
            //TODO:Check Driver Exists If not Add Driver
            tripList.Add(trip);
        }

        public IEnumerable<Trip> GetTripReportData()
        {
            var tripData= from driver in driverList
                          join trip in tripList  on driver.DriverName equals trip.DriverName into leftGroup
                            from item in leftGroup.DefaultIfEmpty(new Trip() { DriverName = string.Empty, Miles = 0 })
                                         .DefaultIfEmpty()
                select new Trip
                {
                    DriverName = driver.DriverName,
                    Miles = item.Miles,
                    MilesPerhour = item.MilesPerhour
                };

            return tripData;
        }
    }
}
