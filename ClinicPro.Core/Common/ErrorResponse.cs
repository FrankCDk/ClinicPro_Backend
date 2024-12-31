using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Core.Common
{
    public class ErrorResponse
    {

        public int StatusCode { get; set; } = 400;
        public string Message { get; set; } = "Error inesperado.";


    }
}
