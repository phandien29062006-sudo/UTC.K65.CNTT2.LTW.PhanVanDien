using DevmasterTrainingManagement.Infrastructure;

namespace DevmasterTrainingManagement.Tests
{
    public class BackupServiceTests
    {
        private readonly BackupService backupService;

        public BackupServiceTests()
        {
            backupService = new BackupService();
        }


        // ==========================================
        // 1. TEST SAO LƯU 1 FILE
        // ==========================================

        [Fact]
        public void BackupFile_ThanhCong()
        {
            string tempFolder =
                Path.Combine(
                    Path.GetTempPath(),
                    "BackupTest");

            string sourceFolder =
                Path.Combine(
                    tempFolder,
                    "Source");

            string backupFolder =
                Path.Combine(
                    tempFolder,
                    "Backup");

            Directory.CreateDirectory(sourceFolder);

            string sourceFile =
                Path.Combine(
                    sourceFolder,
                    "students.json");

            File.WriteAllText(
                sourceFile,
                "[{\"MaHocVien\":\"HV01\"}]");

            bool result =
                backupService.BackupFile(
                    sourceFile,
                    backupFolder);

            string backupFile =
                Path.Combine(
                    backupFolder,
                    "students.json");

            Assert.True(result);
            Assert.True(File.Exists(backupFile));

            Directory.Delete(
                tempFolder,
                true);
        }


        // ==========================================
        // 2. TEST KHÔI PHỤC 1 FILE
        // ==========================================

        [Fact]
        public void RestoreFile_ThanhCong()
        {
            string tempFolder =
                Path.Combine(
                    Path.GetTempPath(),
                    "RestoreTest");

            string backupFolder =
                Path.Combine(
                    tempFolder,
                    "Backup");

            string destinationFolder =
                Path.Combine(
                    tempFolder,
                    "Data");

            Directory.CreateDirectory(
                backupFolder);

            string backupFile =
                Path.Combine(
                    backupFolder,
                    "students.json");

            File.WriteAllText(
                backupFile,
                "[{\"MaHocVien\":\"HV01\"}]");

            bool result =
                backupService.RestoreFile(
                    backupFile,
                    destinationFolder);

            string destinationFile =
                Path.Combine(
                    destinationFolder,
                    "students.json");

            Assert.True(result);
            Assert.True(
                File.Exists(destinationFile));

            Directory.Delete(
                tempFolder,
                true);
        }


        // ==========================================
        // 3. TEST SAO LƯU TOÀN BỘ
        // ==========================================

        [Fact]
        public void BackupAll_ThanhCong()
        {
            string tempFolder =
                Path.Combine(
                    Path.GetTempPath(),
                    "BackupAllTest");

            string dataFolder =
                Path.Combine(
                    tempFolder,
                    "Data");

            string backupFolder =
                Path.Combine(
                    tempFolder,
                    "Backup");

            Directory.CreateDirectory(
                dataFolder);

            string[] files =
            {
                "students.json",
                "courses.json",
                "classes.json",
                "registrations.json",
                "studentcares.json"
            };

            foreach (string file in files)
            {
                File.WriteAllText(
                    Path.Combine(
                        dataFolder,
                        file),
                    "[]");
            }

            bool result =
                backupService.BackupAll(
                    dataFolder,
                    backupFolder);

            Assert.True(result);

            foreach (string file in files)
            {
                string backupFile =
                    Path.Combine(
                        backupFolder,
                        file);

                Assert.True(
                    File.Exists(backupFile));
            }

            Directory.Delete(
                tempFolder,
                true);
        }


        // ==========================================
        // 4. TEST KHÔI PHỤC TOÀN BỘ
        // ==========================================

        [Fact]
        public void RestoreAll_ThanhCong()
        {
            string tempFolder =
                Path.Combine(
                    Path.GetTempPath(),
                    "RestoreAllTest");

            string backupFolder =
                Path.Combine(
                    tempFolder,
                    "Backup");

            string dataFolder =
                Path.Combine(
                    tempFolder,
                    "Data");

            Directory.CreateDirectory(
                backupFolder);

            string[] files =
            {
                "students.json",
                "courses.json",
                "classes.json",
                "registrations.json",
                "studentcares.json"
            };

            foreach (string file in files)
            {
                File.WriteAllText(
                    Path.Combine(
                        backupFolder,
                        file),
                    "[]");
            }

            bool result =
                backupService.RestoreAll(
                    backupFolder,
                    dataFolder);

            Assert.True(result);

            foreach (string file in files)
            {
                string destinationFile =
                    Path.Combine(
                        dataFolder,
                        file);

                Assert.True(
                    File.Exists(destinationFile));
            }

            Directory.Delete(
                tempFolder,
                true);
        }


        // ==========================================
        // 5. FILE NGUỒN KHÔNG TỒN TẠI
        // ==========================================

        [Fact]
        public void BackupFile_FileKhongTonTai()
        {
            string sourceFile =
                Path.Combine(
                    Path.GetTempPath(),
                    "khong_ton_tai.json");

            string backupFolder =
                Path.Combine(
                    Path.GetTempPath(),
                    "BackupTest");

            bool result =
                backupService.BackupFile(
                    sourceFile,
                    backupFolder);

            Assert.False(result);

            if (Directory.Exists(backupFolder))
            {
                Directory.Delete(
                    backupFolder,
                    true);
            }
        }
    }
}