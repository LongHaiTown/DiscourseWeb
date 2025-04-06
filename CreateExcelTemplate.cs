using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;

namespace EduquizSuper
{
    public class CreateExcelTemplate
    {
        public static void Main(string[] args)
        {
            // Đặt license cho EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Đường dẫn đến thư mục templates
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates");
            
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(templatePath))
            {
                Directory.CreateDirectory(templatePath);
            }

            // Đường dẫn đến file Excel mẫu
            string filePath = Path.Combine(templatePath, "question_import_template.xlsx");

            // Tạo file Excel mới
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Tạo worksheet
                var worksheet = package.Workbook.Worksheets.Add("Template");

                // Định dạng tiêu đề
                using (var range = worksheet.Cells[1, 1, 1, 10])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    range.Style.Font.Color.SetColor(System.Drawing.Color.DarkBlue);
                }

                // Thiết lập tiêu đề cột
                worksheet.Cells[1, 1].Value = "ID Câu hỏi";
                worksheet.Cells[1, 2].Value = "Nội dung câu hỏi";
                worksheet.Cells[1, 3].Value = "Độ khó";
                worksheet.Cells[1, 4].Value = "Đáp án 1";
                worksheet.Cells[1, 5].Value = "Đúng/Sai 1";
                worksheet.Cells[1, 6].Value = "Đáp án 2";
                worksheet.Cells[1, 7].Value = "Đúng/Sai 2";
                worksheet.Cells[1, 8].Value = "Đáp án 3";
                worksheet.Cells[1, 9].Value = "Đúng/Sai 3";
                worksheet.Cells[1, 10].Value = "Đáp án 4";
                worksheet.Cells[1, 11].Value = "Đúng/Sai 4";

                // Thêm dữ liệu mẫu
                worksheet.Cells[2, 1].Value = "Q001";
                worksheet.Cells[2, 2].Value = "Thủ đô của Việt Nam là gì?";
                worksheet.Cells[2, 3].Value = "Easy";
                worksheet.Cells[2, 4].Value = "Hà Nội";
                worksheet.Cells[2, 5].Value = "true";
                worksheet.Cells[2, 6].Value = "Hồ Chí Minh";
                worksheet.Cells[2, 7].Value = "false";
                worksheet.Cells[2, 8].Value = "Đà Nẵng";
                worksheet.Cells[2, 9].Value = "false";
                worksheet.Cells[2, 10].Value = "Huế";
                worksheet.Cells[2, 11].Value = "false";

                worksheet.Cells[3, 1].Value = "Q002";
                worksheet.Cells[3, 2].Value = "Ngôn ngữ lập trình nào được sử dụng trong ASP.NET Core?";
                worksheet.Cells[3, 3].Value = "Medium";
                worksheet.Cells[3, 4].Value = "C#";
                worksheet.Cells[3, 5].Value = "true";
                worksheet.Cells[3, 6].Value = "F#";
                worksheet.Cells[3, 7].Value = "true";
                worksheet.Cells[3, 8].Value = "Visual Basic";
                worksheet.Cells[3, 9].Value = "true";
                worksheet.Cells[3, 10].Value = "Python";
                worksheet.Cells[3, 11].Value = "false";

                // Thêm ghi chú
                worksheet.Cells[5, 1].Value = "Lưu ý:";
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[6, 1].Value = "1. ID Câu hỏi phải là duy nhất";
                worksheet.Cells[7, 1].Value = "2. Độ khó phải là một trong các giá trị: Easy, Medium, Hard";
                worksheet.Cells[8, 1].Value = "3. Đúng/Sai phải là 'true' hoặc 'false'";
                worksheet.Cells[9, 1].Value = "4. Mỗi câu hỏi phải có ít nhất một đáp án đúng";

                // Thiết lập độ rộng cột
                worksheet.Column(1).Width = 15;
                worksheet.Column(2).Width = 50;
                worksheet.Column(3).Width = 15;
                worksheet.Column(4).Width = 30;
                worksheet.Column(5).Width = 15;
                worksheet.Column(6).Width = 30;
                worksheet.Column(7).Width = 15;
                worksheet.Column(8).Width = 30;
                worksheet.Column(9).Width = 15;
                worksheet.Column(10).Width = 30;
                worksheet.Column(11).Width = 15;

                // Lưu file
                package.Save();
            }

            Console.WriteLine($"Đã tạo file Excel mẫu tại: {filePath}");
        }
    }
}