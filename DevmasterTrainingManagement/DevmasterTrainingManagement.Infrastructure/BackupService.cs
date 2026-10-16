using System.IO;

namespace DevmasterTrainingManagement.Infrastructure
{
    public class BackupService
    {
        // Sao lưu một file
        public bool BackupFile(
            string sourceFile,
            string backupFolder)
        {
            try
            {
                if (!File.Exists(sourceFile))
                {
                    return false;
                }

                if (!Directory.Exists(backupFolder))
                {
                    Directory.CreateDirectory(backupFolder);
                }

                string fileName =
                    Path.GetFileName(sourceFile);

                string backupFile =
                    Path.Combine(
                        backupFolder,
                        fileName);

                File.Copy(
                    sourceFile,
                    backupFile,
                    true);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // Khôi phục một file
        public bool RestoreFile(
            string backupFile,
            string destinationFolder)
        {
            try
            {
                if (!File.Exists(backupFile))
                {
                    return false;
                }

                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(
                        destinationFolder);
                }

                string fileName =
                    Path.GetFileName(backupFile);

                string destinationFile =
                    Path.Combine(
                        destinationFolder,
                        fileName);

                File.Copy(
                    backupFile,
                    destinationFile,
                    true);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // Sao lưu toàn bộ dữ liệu
        public bool BackupAll(
            string dataFolder,
            string backupFolder)
        {
            try
            {
                if (!Directory.Exists(dataFolder))
                {
                    return false;
                }

                if (!Directory.Exists(backupFolder))
                {
                    Directory.CreateDirectory(
                        backupFolder);
                }

                string[] files =
                {
                    "students.json",
                    "courses.json",
                    "classes.json",
                    "registrations.json",
                    "studentcares.json"
                };

                foreach (string fileName in files)
                {
                    string sourceFile =
                        Path.Combine(
                            dataFolder,
                            fileName);

                    if (File.Exists(sourceFile))
                    {
                        string backupFile =
                            Path.Combine(
                                backupFolder,
                                fileName);

                        File.Copy(
                            sourceFile,
                            backupFile,
                            true);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        // Khôi phục toàn bộ dữ liệu
        public bool RestoreAll(
            string backupFolder,
            string dataFolder)
        {
            try
            {
                if (!Directory.Exists(backupFolder))
                {
                    return false;
                }

                if (!Directory.Exists(dataFolder))
                {
                    Directory.CreateDirectory(
                        dataFolder);
                }

                string[] files =
                {
                    "students.json",
                    "courses.json",
                    "classes.json",
                    "registrations.json",
                    "studentcares.json"
                };

                foreach (string fileName in files)
                {
                    string backupFile =
                        Path.Combine(
                            backupFolder,
                            fileName);

                    if (File.Exists(backupFile))
                    {
                        string destinationFile =
                            Path.Combine(
                                dataFolder,
                                fileName);

                        File.Copy(
                            backupFile,
                            destinationFile,
                            true);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}