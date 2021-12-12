using System;
using Microsoft.Extensions.Logging;

namespace MyService
{
    public class Service
    {
        private readonly ILogger _logger;

        public Service(ILogger logger)
        {
            _logger = logger;
        }

        public void ThrowAnException()
        {
            try
            {
                var errorMessage = $"Some fishy error";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred in {nameof(ThrowAnException)}");
            }
        }

        public void GenerateAWarning()
        {
            _logger.LogWarning("This is a warning");
        }
    }
}
