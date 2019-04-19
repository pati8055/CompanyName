using System;
using System.Collections.Generic;
using System.Text;

namespace CodeKata.Entities
{
    public class Trip
    {
        public string  DriverName { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan StopTime { get; set; }

        public double Miles { get; set; }

        public double MilesPerhour { get; set; }
    }

    public class Driver
    {
        public string DriverName { get; set; }
    }

    public class ReportData
    {
        public string DriverName { get; set; }
        
        public double MilesPerhour { get; set; }

        public double Miles { get; set; }
    }

    public enum CommandType
    {
        Driver,
        Trip
    }
}
