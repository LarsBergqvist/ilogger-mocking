using Microsoft.Extensions.Logging;
using Xunit;

namespace MyService.Tests
{
    public class ServiceTests
    {
        private readonly MockLogger _mockLogger;

        public ServiceTests()
        {
            _mockLogger = new MockLogger();
        }

        [Fact]
        public void ErrorsShouldBeLogged()
        {
            var service = new Service(_mockLogger);
            service.ThrowAnException();
            Assert.True(_mockLogger.ReceivedNTimes(1, LogLevel.Error, new string[1] {"Some fishy error"}));
            Assert.True(_mockLogger.ReceivedNTimes(1, LogLevel.Error, new string[1] {"An error occurred in ThrowAnException"}));
            Assert.True(_mockLogger.ReceivedNTimes(2, LogLevel.Error, new string[1] {"error"}));
        }

        [Fact]
        public void WarningShouldBeLogged()
        {
            var service = new Service(_mockLogger);
            service.GenerateAWarning();
            Assert.True(_mockLogger.ReceivedNTimes(1, LogLevel.Warning, new string[1] {"This is a warning"}));
        }

    }
}
