using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiAngularAdDemo.BusinessComponent.Utilities
{
    public static class ExcelExtension
    {
        public static bool IsWorkSheetExists(this ExcelWorkbook excelWorkbook, string workSheetName)
        {
            return excelWorkbook.Worksheets.Any(workSheet => workSheet.Name == workSheetName);
        }
    }
}
