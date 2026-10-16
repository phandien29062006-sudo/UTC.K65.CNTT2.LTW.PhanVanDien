using System.Globalization;
using System.Reflection;
using System.Text;

namespace DevmasterTrainingManagement.Infrastructure
{
    public class CsvService
    {
        // ==============================
        // EXPORT CSV
        // ==============================
        public bool ExportCsv<T>(
            string filePath,
            List<T> data)
        {
            try
            {
                if (data == null || data.Count == 0)
                {
                    return false;
                }

                PropertyInfo[] properties =
                    typeof(T).GetProperties();

                StringBuilder csv =
                    new StringBuilder();

                // Ghi dòng tiêu đề
                csv.AppendLine(
                    string.Join(",",
                        properties.Select(p =>
                            EscapeCsv(p.Name))));

                // Ghi dữ liệu
                foreach (T item in data)
                {
                    string line =
                        string.Join(",",
                            properties.Select(p =>
                            {
                                object? value =
                                    p.GetValue(item);

                                return EscapeCsv(
                                    Convert.ToString(
                                        value,
                                        CultureInfo.InvariantCulture)
                                    ?? "");
                            }));

                    csv.AppendLine(line);
                }

                File.WriteAllText(
                    filePath,
                    csv.ToString(),
                    new UTF8Encoding(true));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Lỗi export CSV: " +
                    ex.Message);

                return false;
            }
        }


        // ==============================
        // IMPORT CSV
        // ==============================
        public List<T> ImportCsv<T>(
            string filePath)
        {
            List<T> result =
                new List<T>();

            try
            {
                if (!File.Exists(filePath))
                {
                    return result;
                }

                string[] lines =
                    File.ReadAllLines(
                        filePath,
                        Encoding.UTF8);

                if (lines.Length <= 1)
                {
                    return result;
                }

                List<string> headers =
                    ParseCsvLine(lines[0]);

                PropertyInfo[] properties =
                    typeof(T).GetProperties();

                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                    {
                        continue;
                    }

                    List<string> values =
                        ParseCsvLine(lines[i]);

                    T item =
                        Activator.CreateInstance<T>();

                    for (int j = 0;
                         j < headers.Count &&
                         j < values.Count;
                         j++)
                    {
                        PropertyInfo? property =
                            properties.FirstOrDefault(
                                p => p.Name.Equals(
                                    headers[j],
                                    StringComparison.OrdinalIgnoreCase));

                        if (property == null ||
                            !property.CanWrite)
                        {
                            continue;
                        }

                        object? value =
                            ConvertValue(
                                values[j],
                                property.PropertyType);

                        property.SetValue(
                            item,
                            value);
                    }

                    result.Add(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Lỗi import CSV: " +
                    ex.Message);

                return result;
            }
        }


        // ==============================
        // XỬ LÝ GIÁ TRỊ CSV
        // ==============================
        private string EscapeCsv(
            string value)
        {
            if (value.Contains(",") ||
                value.Contains("\"") ||
                value.Contains("\n"))
            {
                value =
                    value.Replace(
                        "\"",
                        "\"\"");

                return "\"" +
                       value +
                       "\"";
            }

            return value;
        }


        // ==============================
        // ĐỌC 1 DÒNG CSV
        // ==============================
        private List<string> ParseCsvLine(
            string line)
        {
            List<string> result =
                new List<string>();

            StringBuilder current =
                new StringBuilder();

            bool insideQuotes = false;

            for (int i = 0;
                 i < line.Length;
                 i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (insideQuotes &&
                        i + 1 < line.Length &&
                        line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        insideQuotes =
                            !insideQuotes;
                    }
                }
                else if (c == ',' &&
                         !insideQuotes)
                {
                    result.Add(
                        current.ToString());

                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            result.Add(
                current.ToString());

            return result;
        }


        // ==============================
        // CHUYỂN KIỂU DỮ LIỆU
        // ==============================
        private object? ConvertValue(
            string value,
            Type targetType)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (Nullable.GetUnderlyingType(
                        targetType) != null)
                {
                    return null;
                }

                if (targetType == typeof(string))
                {
                    return "";
                }

                return Activator.CreateInstance(
                    targetType);
            }

            Type actualType =
                Nullable.GetUnderlyingType(
                    targetType)
                ?? targetType;

            if (actualType == typeof(string))
            {
                return value;
            }

            if (actualType == typeof(DateTime))
            {
                return DateTime.Parse(
                    value,
                    CultureInfo.InvariantCulture);
            }

            if (actualType == typeof(decimal))
            {
                return decimal.Parse(
                    value,
                    CultureInfo.InvariantCulture);
            }

            if (actualType == typeof(double))
            {
                return double.Parse(
                    value,
                    CultureInfo.InvariantCulture);
            }

            if (actualType == typeof(int))
            {
                return int.Parse(
                    value,
                    CultureInfo.InvariantCulture);
            }

            if (actualType == typeof(bool))
            {
                return bool.Parse(value);
            }

            if (actualType.IsEnum)
            {
                return Enum.Parse(
                    actualType,
                    value);
            }

            return Convert.ChangeType(
                value,
                actualType,
                CultureInfo.InvariantCulture);
        }
    }
}