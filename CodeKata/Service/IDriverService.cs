using System.Collections.Generic;
using CodeKata.Entities;

namespace CodeKata.Service
{
    public interface IDriverService
    {
        void UploadFile(string line);
        List<ReportData> GenerateReport();

    }
}