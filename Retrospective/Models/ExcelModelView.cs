using System.Web;

namespace Retrospective.Models
{
    public class ExcelModel
    {
        public string FilePath { get; set; }
        public HttpPostedFileBase ExcelFile { get; set; }
    }
}