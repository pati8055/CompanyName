using CodeKata.BusinessEngine;
using CodeKata.DataAccess;
using CodeKata.DataAccess.Interface;
using CodeKata.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using CodeKata.Entities;

namespace CodeKata
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped<IDriverService, DriverEngine>()
                .AddScoped<IDriverRepository, InMemoryData>() //Once Database is available Map it here
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");


            var driverService = serviceProvider.GetService<IDriverService>();

            string filePath = Environment.CurrentDirectory + @"\\InputFile.txt";

            if (File.Exists(filePath))
            {
                StreamReader file = null;
                try
                {
                    file = new StreamReader(filePath);
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        driverService.UploadFile(line);
                    }
                }
                finally
                {
                    file?.Close();
                }
            }

            List<ReportData> reportDataList =driverService.GenerateReport();

            foreach (var reportData in reportDataList)
            {
                Console.WriteLine($"{reportData.DriverName}:{reportData.Miles} miles @ {reportData.MilesPerhour} mph");
            }

            Console.ReadLine();
        }
        
    }
}
