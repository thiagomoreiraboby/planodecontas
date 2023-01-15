using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace planodecontas.application.Utils
{
    public class GenericResult
    {
        public GenericResult()
        {
            Success = true;
        }
        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }
        public object? Content { get; set; }
    }
}
