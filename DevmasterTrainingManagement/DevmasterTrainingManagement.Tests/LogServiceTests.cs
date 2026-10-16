using DevmasterTrainingManagement.Infrastructure;

namespace DevmasterTrainingManagement.Tests
{
    public class LogServiceTests
    {
        // ==========================================
        // 1. TEST GHI LOG LỖI
        // ==========================================

        [Fact]
        public void LogError_ThanhCong()
        {
            string logFile =
                Path.Combine(
                    Path.GetTempPath(),
                    "test_error.log");

            if (File.Exists(logFile))
            {
                File.Delete(logFile);
            }

            LogService logService =
                new LogService(logFile);

            bool result =
                logService.LogError(
                    "Lỗi test");

            Assert.True(result);
            Assert.True(
                File.Exists(logFile));

            string content =
                File.ReadAllText(logFile);

            Assert.Contains(
                "ERROR: Lỗi test",
                content);

            File.Delete(logFile);
        }


        // ==========================================
        // 2. TEST GHI LOG THÔNG TIN
        // ==========================================

        [Fact]
        public void LogInfo_ThanhCong()
        {
            string logFile =
                Path.Combine(
                    Path.GetTempPath(),
                    "test_info.log");

            if (File.Exists(logFile))
            {
                File.Delete(logFile);
            }

            LogService logService =
                new LogService(logFile);

            bool result =
                logService.LogInfo(
                    "Chương trình đã khởi động");

            Assert.True(result);
            Assert.True(
                File.Exists(logFile));

            string content =
                File.ReadAllText(logFile);

            Assert.Contains(
                "INFO: Chương trình đã khởi động",
                content);

            File.Delete(logFile);
        }


        // ==========================================
        // 3. TEST GHI NHIỀU LOG
        // ==========================================

        [Fact]
        public void LogError_NhieuLan()
        {
            string logFile =
                Path.Combine(
                    Path.GetTempPath(),
                    "test_multiple.log");

            if (File.Exists(logFile))
            {
                File.Delete(logFile);
            }

            LogService logService =
                new LogService(logFile);

            logService.LogError("Lỗi 1");
            logService.LogError("Lỗi 2");
            logService.LogInfo("Thông tin 1");

            string content =
                File.ReadAllText(logFile);

            Assert.Contains(
                "ERROR: Lỗi 1",
                content);

            Assert.Contains(
                "ERROR: Lỗi 2",
                content);

            Assert.Contains(
                "INFO: Thông tin 1",
                content);

            File.Delete(logFile);
        }
    }
}