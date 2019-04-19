using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CodeKata.DataAccess.Interface;
using CodeKata.Entities;
using CodeKata.Service;
using Microsoft.Extensions.Logging;

namespace CodeKata.BusinessEngine
{
    class DriverEngine : IDriverService
    {
        private readonly ILogger<DriverEngine> _logger;
        private readonly IDriverRepository _driverRepository;

        public DriverEngine(ILoggerFactory loggerFactory, IDriverRepository driverRepository)
        {
            _logger = loggerFactory.CreateLogger<DriverEngine>();
            _driverRepository = driverRepository;
        }

        public List<ReportData> GenerateReport()
        {
            List<Trip> tripSummaryData = _driverRepository.GetTripReportData().ToList();

            //TODO:Discard any trips that average a speed of less than 5 mph or greater than 100 mph. - would Add Where Clause on colelction
            
            var groupedTrips = tripSummaryData.GroupBy(gb => new { gb.DriverName })
                .OrderByDescending(g => g.Max(x => x.Miles)).Select(s => new Trip()
                {
                    DriverName = s.Key.DriverName,
                    Miles = Math.Round(s.Sum(x => x.Miles)),
                    MilesPerhour = Math.Round(s.Average(x => x.MilesPerhour))
                });

            var reportDatas = groupedTrips.Select(MapToReportData).ToList();

            return reportDatas;
        }

        public void UploadFile(string line)
        {
            string[] inputData = line.Split(' ');
            if (inputData[0] == CommandType.Driver.ToString()) //For now assuming Only 2 command Types.Switch Statement is better
            {
                _driverRepository.RegisterDriver(new Driver
                {
                    DriverName = inputData[1]
                });
            }
            else
            {
                //Some Helper Method with TryParse for Conversion For TypeSafety
                Trip newTrip = new Trip()
                {
                    DriverName = inputData[1],
                    StartTime = DateTime.ParseExact(inputData[2], "HH:mm", new CultureInfo("en-US")).TimeOfDay,
                    StopTime = DateTime.ParseExact(inputData[3], "HH:mm", new CultureInfo("en-US")).TimeOfDay,
                    Miles = Convert.ToDouble(inputData[4])

                };

                newTrip.MilesPerhour = newTrip.Miles > 0 ? CalculateAverageMiles(newTrip):0;
                _driverRepository.RegisterTrip(newTrip);
            }

        }

        private static double CalculateAverageMiles(Trip tripData)
        {
            //Assumin MPH=distance/time
            double timeHours = (tripData.StopTime - tripData.StartTime).TotalHours;

            double mph = Math.Round(tripData.Miles/timeHours);

            return mph;
        }

        public static readonly Func<Trip, ReportData> MapToReportData = s => new ReportData()
        {
            DriverName = s.DriverName,
            Miles = s.Miles,
            MilesPerhour = s.MilesPerhour
        };
    }
}
