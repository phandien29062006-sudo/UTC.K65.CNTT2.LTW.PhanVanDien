# Devmaster Training Management

## 1. Mục tiêu

Xây dựng ứng dụng quản lý trung tâm đào tạo Devmaster bằng C#, hỗ trợ quản lý khóa học, lớp học, học viên, đăng ký học, thanh toán và chăm sóc học viên.

Ứng dụng được tổ chức theo kiến trúc nhiều project, có lưu trữ dữ liệu JSON, Import/Export CSV, sao lưu/khôi phục dữ liệu và Unit Test.

## 2. Công nghệ

- C#
- .NET 10
- Console Application
- LINQ
- System.Text.Json
- CSV
- xUnit
- Visual Studio
- Git/GitHub

## 3. Chức năng

### Quản lý khóa học
- Thêm, hiển thị, tìm kiếm, cập nhật ,xóa khóa học, sắp xếp và thống kê học phí

### Quản lý lớp học
- Thêm, hiển thị, tìm kiếm, cập nhật ,xóa lớp học và kiểm tra sĩ số.

### Quản lý học viên
- Thêm, hiển thị, tìm kiếm, cập nhật,  xóa học viên và import từ csv, export sang csv và lưu dữ liệu json

### Quản lý đăng ký
- Đăng ký học.
- Kiểm tra lớp còn chỗ.
- Kiểm tra học viên đã đăng ký.
- Tính học phí còn thiếu.
- Thanh toán học phí.
- Hủy đăng ký.
- Thống kê công nợ.

### Chăm sóc học viên
- Ghi nhận lịch sử chăm sóc.
- Xem lịch sử chăm sóc theo học viên.
- Xem lịch hẹn hôm nay.
- Xem lịch hẹn quá hạn.
- Thống kê kết quả chăm sóc.

### Báo cáo
Ứng dụng hỗ trợ 10 báo cáo:
1. Số học viên theo khóa học.
2. Số học viên theo lớp.
3. Danh sách lớp sắp khai giảng.
4. Danh sách học viên còn nợ học phí.
5. Tổng doanh thu.
6. Doanh thu theo tháng.
7. Khóa học có nhiều học viên nhất.
8. Học viên có lịch hẹn hôm nay.
9. Học viên lâu ngày chưa được chăm sóc.
10. Tỷ lệ học viên đã thanh toán đủ.

### Lưu trữ dữ liệu
- Lưu dữ liệu JSON.
- Đọc dữ liệu khi khởi động.
- Import CSV.
- Export CSV.
- Sao lưu dữ liệu.
- Khôi phục dữ liệu.
- Ghi log lỗi.

### Kiểm thử
- Unit Test cho các service chính.
- Unit Test cho lưu trữ, backup và báo cáo.
- Các test đã chạy thành công.

## 4. Cấu trúc project

DevmasterTrainingManagement.sln
│
├── DevmasterTrainingManagement.Domain
│   └── Student, Course, ClassRoom, Registration, StudentCare
│
├── DevmasterTrainingManagement.Application
│   └── Các service xử lý nghiệp vụ
│
├── DevmasterTrainingManagement.Infrastructure
│   └── StorageService, CsvService, BackupService, LogService
│
├── DevmasterTrainingManagement.ConsoleUI
│   └── Program.cs và giao diện Console
│
└── DevmasterTrainingManagement.Tests
    └── Unit Test

## 5. Dữ liệu mẫu

Dữ liệu JSON:

```text
students.json
courses.json
classes.json
registrations.json
studentcares.json
```

Dữ liệu CSV:

```text
students.csv
courses.csv
classes.csv
registrations.csv
studentcares.csv
```

## 6. Hướng dẫn chạy

### Yêu cầu

- Visual Studio
- .NET 10 SDK

### Các bước

1. Mở `DevmasterTrainingManagement.sln`.
2. Chọn `DevmasterTrainingManagement.ConsoleUI` làm Startup Project.
3. Build bằng `Ctrl + Shift + B`.
4. Chạy bằng `Ctrl + F5`.
5. Khi khởi động, chương trình tự động đọc dữ liệu từ JSON.
6. Khi chọn `0. Thoát`, chương trình lưu toàn bộ dữ liệu ra JSON.

## 7. Unit Test

Project:

text
DevmasterTrainingManagement.Tests

Các nhóm test:

- StudentServiceTests
- RegistrationServiceTests
- ClassServiceTests
- CourseServiceTests
- StudentCareServiceTests
- ReportServiceTests
- StorageServiceTests
- BackupServiceTests

Chạy toàn bộ test trong Visual Studio:

Test → Run All Tests

Kết quả: các test đã chạy thành công.

## 8. Test case

| Chức năng | Kết quả |
|---|---|
| Thêm học viên | Passed |
| Thêm khóa học | Passed |
| Thêm lớp học | Passed |
| Đăng ký học | Passed |
| Thanh toán học phí | Passed |
| Hủy đăng ký | Passed |
| Chăm sóc học viên | Passed |
| 10 báo cáo | Passed |
| Lưu JSON | Passed |
| Đọc JSON | Passed |
| Import CSV | Passed |
| Export CSV | Passed |
| Backup dữ liệu | Passed |
| Restore dữ liệu | Passed |
| Unit Test | Passed |

## 9. Screenshot

Ảnh chụp kết quả chạy chương trình được bổ sung trong thư mục:
Screenshots/

Các màn hình chính:
- Menu chính.
- Quản lý học viên.
- Quản lý khóa học.
- Quản lý lớp học.
- Đăng ký và thanh toán.
- Chăm sóc học viên.
- 10 báo cáo.



