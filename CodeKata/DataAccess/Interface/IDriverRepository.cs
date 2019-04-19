using System;
using System.Collections.Generic;
using System.Text;
using CodeKata.Entities;

namespace CodeKata.DataAccess.Interface
{
    public interface IDriverRepository
    {
        void RegisterDriver(Driver driver);

        void RegisterTrip(Trip trip);

        IEnumerable<Trip> GetTripReportData();


    }
}
