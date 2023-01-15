using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace planodecontas.application.Utils
{
    public static class Exceptions
    {
        public static void ProcessException<T>(this ILogger<T> logger, GenericResult result, string message)
        {
            result.ErrorMessage = message;
            result.Success = false;

            logger.LogError(message);
        }
    }
}
