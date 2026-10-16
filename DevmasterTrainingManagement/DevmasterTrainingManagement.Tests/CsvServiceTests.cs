using DevmasterTrainingManagement.Infrastructure;

namespace DevmasterTrainingManagement.Tests
{
    public class CsvServiceTests
    {
        private readonly CsvService csvService;

        public CsvServiceTests()
        {
            csvService = new CsvService();
        }

        [Fact]
        public void ExportCsv_ThanhCong()
        {
            string filePath =
                "test_students.csv";

            List<TestStudent> students =
                new List<TestStudent>
                {
                    new TestStudent
                    {
                        MaHocVien = "HV01",
                        HoTen = "Nguyen Van A",
                        Tuoi = 20
                    },
                    new TestStudent
                    {
                        MaHocVien = "HV02",
                        HoTen = "Tran Thi B",
                        Tuoi = 21
                    }
                };

            bool result =
                csvService.ExportCsv(
                    filePath,
                    students);

            Assert.True(result);
            Assert.True(
                File.Exists(filePath));

            File.Delete(filePath);
        }


        [Fact]
        public void ImportCsv_ThanhCong()
        {
            string filePath =
                "test_students.csv";

            File.WriteAllText(
                filePath,
                "MaHocVien,HoTen,Tuoi\n" +
                "HV01,Nguyen Van A,20\n" +
                "HV02,Tran Thi B,21");

            List<TestStudent> result =
                csvService.ImportCsv<TestStudent>(
                    filePath);

            Assert.Equal(
                2,
                result.Count);

            Assert.Equal(
                "HV01",
                result[0].MaHocVien);

            Assert.Equal(
                "Nguyen Van A",
                result[0].HoTen);

            Assert.Equal(
                20,
                result[0].Tuoi);

            File.Delete(filePath);
        }


        // Class dùng riêng cho Unit Test
        private class TestStudent
        {
            public string MaHocVien { get; set; } = "";
            public string HoTen { get; set; } = "";
            public int Tuoi { get; set; }
        }
    }
}