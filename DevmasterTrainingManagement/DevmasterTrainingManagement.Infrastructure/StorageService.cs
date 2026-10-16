using System.Text;
using System.Text.Json;

namespace DevmasterTrainingManagement.Infrastructure
{
    public class StorageService
    {
        private readonly JsonSerializerOptions options =
            new JsonSerializerOptions
            {
                WriteIndented = true
            };

        // Lưu dữ liệu ra file JSON
        public bool SaveJson<T>(
            string filePath,
            List<T> data)
        {
            try
            {
                string json =
                    JsonSerializer.Serialize(
                        data,
                        options);

                File.WriteAllText(
                    filePath,
                    json,
                    Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Lỗi lưu dữ liệu: " + ex.Message);

                return false;
            }
        }

        // Đọc dữ liệu từ file JSON
        public List<T> LoadJson<T>(
            string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }

                string json =
                    File.ReadAllText(
                        filePath,
                        Encoding.UTF8);

                List<T>? data =
                    JsonSerializer.Deserialize<List<T>>(
                        json);

                return data ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Lỗi đọc dữ liệu: " + ex.Message);

                return new List<T>();
            }
        }
    }
}