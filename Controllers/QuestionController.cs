using EduquizSuper.Data;
using EduquizSuper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduquizSuper.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly EduquizContextDb _context;

        public QuestionController(EduquizContextDb context)
        {
            _context = context;
        }

        // GET: Question
        public async Task<IActionResult> Index(string searchString, string currentFilter, string difficultyFilter, int? pageNumber, string subjectId = null)
        {
            if (searchString != null || difficultyFilter != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSubject"] = subjectId;
            ViewData["CurrentDifficulty"] = difficultyFilter;

            // Gán danh sách môn học
            ViewBag.SubjectId = new SelectList(_context.Subjects, "SubjectId", "SubjectName");

            // Gán danh sách độ khó
            ViewData["DifficultyLevels"] = new List<string> { "Easy", "Medium", "Hard" };

            var questions = _context.Questions.Include(q => q.Subject)
                                              .AsQueryable();

            if (!string.IsNullOrEmpty(subjectId))
            {
                questions = questions.Where(q => q.SubjectId == subjectId);
            }

            if (!string.IsNullOrEmpty(difficultyFilter))
            {
                questions = questions.Where(q => q.Difficulty == difficultyFilter);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(q => q.QuestionContent.Contains(searchString));
            }

            int pageSize = 500;
            return View(await PaginatedList<Question>.CreateAsync(questions.OrderBy(q => q.SubjectId), pageNumber ?? 1, pageSize));
        }

        // GET: Question/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                return RedirectToAction(nameof(Index));
            }

            var question = await _context.Questions
                .Include(q => q.Subject)
                .FirstOrDefaultAsync(m => m.QuestionId == id);

            if (question == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                return RedirectToAction(nameof(Index));
            }

            // Chuyển đổi JSON thành danh sách câu trả lời
            try
            {
                var answers = string.IsNullOrEmpty(question.AnswersJson)
                    ? new List<dynamic>()
                    : JsonConvert.DeserializeObject<List<dynamic>>(question.AnswersJson);
                ViewData["Answers"] = answers;
            }
            catch (JsonException)
            {
                TempData["ErrorMessage"] = "Dữ liệu câu trả lời không hợp lệ.";
                ViewData["Answers"] = new List<dynamic>();
            }

            return View(question);
        }

        // GET: Question/Create
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            ViewData["DifficultyLevels"] = new List<string> { "Easy", "Medium", "Hard" };
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([Bind("QuestionId,QuestionContent,SubjectId,Difficulty")] Question question, List<string> answerTexts, int correctAnswer)
        {
            // Debug
            Console.WriteLine($"QuestionId: {question.QuestionId}");
            Console.WriteLine($"SubjectId: {question.SubjectId}");
            Console.WriteLine($"Difficulty: {question.Difficulty}");
            Console.WriteLine($"QuestionContent: {question.QuestionContent}");
            Console.WriteLine($"answerTexts Count: {answerTexts?.Count ?? 0}");
            if (answerTexts != null)
            {
                for (int i = 0; i < answerTexts.Count; i++)
                {
                    Console.WriteLine($"answerTexts[{i}]: {answerTexts[i]}");
                }
            }
            Console.WriteLine($"correctAnswer: {correctAnswer}");

            // Xóa lỗi không cần thiết từ ModelState
            if (ModelState.ContainsKey("Subject"))
            {
                ModelState["Subject"].Errors.Clear();
                ModelState["Subject"].ValidationState = ModelValidationState.Valid;
            }
            if (ModelState.ContainsKey("AnswersJson"))
            {
                ModelState["AnswersJson"].Errors.Clear();
                ModelState["AnswersJson"].ValidationState = ModelValidationState.Valid;
            }

            // Kiểm tra QuestionId có trùng không
            if (_context.Questions.Any(q => q.QuestionId == question.QuestionId))
            {
                ModelState.AddModelError("QuestionId", "Mã câu hỏi đã tồn tại. Vui lòng chọn mã khác.");
            }

            if (ModelState.IsValid && answerTexts?.Count == 4)
            {
                try
                {
                    // Tạo danh sách câu trả lời
                    var answers = new List<dynamic>();
                    for (int i = 0; i < answerTexts.Count; i++)
                    {
                        answers.Add(new
                        {
                            id = (i + 1).ToString(),
                            content = answerTexts[i],
                            isCorrect = i == correctAnswer
                        });
                    }

                    // Chuyển thành JSON
                    question.AnswersJson = JsonConvert.SerializeObject(answers);

                    _context.Add(question);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "Tạo câu hỏi thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo câu hỏi: {ex.Message}");
                }
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                        }
                    }
                }
                if (answerTexts?.Count != 4)
                {
                    ModelState.AddModelError("", "Phải cung cấp đúng 4 đáp án.");
                }
            }

            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", question.SubjectId);
            ViewData["DifficultyLevels"] = new List<string> { "Easy", "Medium", "Hard" };
            return View(question);
        }

        // GET: Question/Edit/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                return RedirectToAction(nameof(Index));
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                return RedirectToAction(nameof(Index));
            }

            // Chuyển đổi JSON thành danh sách câu trả lời
            try
            {
                var answers = string.IsNullOrEmpty(question.AnswersJson)
                    ? new List<dynamic>()
                    : JsonConvert.DeserializeObject<List<dynamic>>(question.AnswersJson);
                ViewData["Answers"] = answers;
            }
            catch (JsonException)
            {
                TempData["ErrorMessage"] = "Dữ liệu câu trả lời không hợp lệ.";
                ViewData["Answers"] = new List<dynamic>();
            }

            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", question.SubjectId);
            ViewData["DifficultyLevels"] = new List<string> { "Easy", "Medium", "Hard" };
            return View(question);
        }

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(string id, [Bind("QuestionId,QuestionContent,SubjectId,Difficulty")] Question question, List<string> answerTexts, List<string> isCorrect)
        {
            // Debug: In ra thông tin đầu vào
            Console.WriteLine($"[Debug] Edit - ID: {id}");
            Console.WriteLine($"[Debug] Question ID: {question.QuestionId}");
            Console.WriteLine($"[Debug] Subject ID: {question.SubjectId}");
            Console.WriteLine($"[Debug] Difficulty: {question.Difficulty}");
            Console.WriteLine($"[Debug] Answer Texts Count: {answerTexts?.Count ?? 0}");
            //Console.WriteLine($"[Debug] Is Correct Count: {isCorrect?.Length ?? 0}");

            if (id != question.QuestionId)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                return RedirectToAction(nameof(Index));
            }

            // Kiểm tra xem câu hỏi có trong bài thi nào không
            var existingQuestion = await _context.Questions
                .Include(q => q.ExamDetails)
                .FirstOrDefaultAsync(q => q.QuestionId == id);

            if (existingQuestion != null && existingQuestion.ExamDetails != null && existingQuestion.ExamDetails.Any())
            {
                Console.WriteLine($"[Debug] Câu hỏi đang được sử dụng trong {existingQuestion.ExamDetails.Count} bài thi");
                
                // Nếu thay đổi môn học và câu hỏi đang được sử dụng trong bài thi
                if (existingQuestion.SubjectId != question.SubjectId)
                {
                    Console.WriteLine($"[Debug] Cố gắng thay đổi môn học từ {existingQuestion.SubjectId} sang {question.SubjectId}");
                    TempData["ErrorMessage"] = "Không thể thay đổi môn học của câu hỏi vì nó đang được sử dụng trong bài thi.";
                    ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", existingQuestion.SubjectId);
                    ViewData["DifficultyLevels"] = new List<string> { "Easy", "Medium", "Hard" };
                    return View(existingQuestion);
                }
            }

            // Kiểm tra và xử lý dữ liệu đầu vào
            if (answerTexts == null || answerTexts.Count < 2)
            {
                ModelState.AddModelError("", "Câu hỏi phải có ít nhất 2 câu trả lời.");
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", question.SubjectId);
                ViewData["DifficultyLevels"] = new List<string> { "Easy", "Medium", "Hard" };
                return View(question);
            }

            // Khởi tạo danh sách isCorrect nếu null
            isCorrect = isCorrect?.ToList() ?? new List<string>();

            // Chuyển đổi isCorrect từ List<string> sang List<bool>
            var isCorrectBools = new List<bool>();
            for (int i = 0; i < answerTexts.Count; i++)
            {
                isCorrectBools.Add(isCorrect.Contains(i.ToString()));
            }

            // Kiểm tra có ít nhất 1 câu trả lời đúng
            if (!isCorrectBools.Any(x => x))
            {
                ModelState.AddModelError("", "Câu hỏi phải có ít nhất 1 câu trả lời đúng.");
                return View(question);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo danh sách câu trả lời
                    var answers = new List<dynamic>();
                    for (int i = 0; i < answerTexts.Count; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(answerTexts[i]))
                        {
                            answers.Add(new
                            {
                                id = (i + 1).ToString(),
                                content = answerTexts[i].Trim(),
                                isCorrect = isCorrectBools[i]
                            });
                        }
                    }

                    // Kiểm tra số lượng câu trả lời sau khi lọc
                    if (answers.Count < 2)
                    {
                        ModelState.AddModelError("", "Câu hỏi phải có ít nhất 2 câu trả lời có nội dung.");
                        return View(question);
                    }

                    // Cập nhật câu hỏi
                    question.AnswersJson = JsonConvert.SerializeObject(answers);
                    _context.Update(question);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "Cập nhật câu hỏi thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                        return RedirectToAction(nameof(Index));
                    }
                    throw;
                }
            }

            // Chuẩn bị dữ liệu cho view khi có lỗi
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", question.SubjectId);
            ViewData["DifficultyLevels"] = new List<string> { "Easy", "Medium", "Hard" };
            ViewData["Answers"] = answerTexts.Select((text, index) => new
            {
                id = (index + 1).ToString(),
                content = text,
                isCorrect = isCorrectBools[index]
            }).ToList();

            return View(question);
        }
        // GET: Question/Delete/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                return RedirectToAction(nameof(Index));
            }

            var question = await _context.Questions
                .Include(q => q.Subject)
                .FirstOrDefaultAsync(m => m.QuestionId == id);

            if (question == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                return RedirectToAction(nameof(Index));
            }

            // Kiểm tra xem câu hỏi có đang được sử dụng trong bài thi không
            var isUsedInExam = await _context.ExamDetails.AnyAsync(ed => ed.QuestionId == id);
            if (isUsedInExam)
            {
                TempData["ErrorMessage"] = "Câu hỏi đang được sử dụng trong một bài thi, không thể xóa.";
                return RedirectToAction(nameof(Index));
            }

            // Chuyển đổi JSON thành danh sách câu trả lời
            try
            {
                var answers = string.IsNullOrEmpty(question.AnswersJson)
                    ? new List<dynamic>()
                    : JsonConvert.DeserializeObject<List<dynamic>>(question.AnswersJson);
                ViewData["Answers"] = answers;
            }
            catch (JsonException)
            {
                TempData["ErrorMessage"] = "Dữ liệu câu trả lời không hợp lệ.";
                ViewData["Answers"] = new List<dynamic>();
            }

            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi.";
                return RedirectToAction(nameof(Index));
            }

            // Kiểm tra lại xem câu hỏi có đang được sử dụng trong bài thi không
            var isUsedInExam = await _context.Questions.AnyAsync(eq => eq.QuestionId == id);
            if (isUsedInExam)
            {
                TempData["ErrorMessage"] = "Câu hỏi đang được sử dụng trong một bài thi, không thể xóa.";
                return RedirectToAction(nameof(Index));
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Xóa câu hỏi thành công!";
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(string id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
        // GET: Question/Import
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Import()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: Question/Import
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Import(IFormFile file, string subjectId)
        {
            // Kiểm tra cơ bản
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn file để import.";
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
                return View();
            }

            if (string.IsNullOrEmpty(subjectId))
            {
                TempData["ErrorMessage"] = "Vui lòng chọn môn học.";
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
                return View();
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".csv" && extension != ".xlsx")
            {
                TempData["ErrorMessage"] = "Chỉ hỗ trợ file CSV hoặc Excel (.xlsx).";
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
                return View();
            }

            var importedQuestions = new List<Question>();
            var failedRows = new List<string>();
            var successCount = 0;

            try
            {
                if (extension == ".xlsx")
                {
                    ExcelPackage.License.SetNonCommercialPersonal("YourName"); // Cấu hình license nếu dùng EPPlus 8.x
                    using (var package = new ExcelPackage(file.OpenReadStream()))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        if (worksheet == null || worksheet.Dimension == null)
                        {
                            TempData["ErrorMessage"] = "File Excel không hợp lệ hoặc không có dữ liệu.";
                            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
                            return View();
                        }

                        int rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            try // Try-catch riêng cho từng dòng
                            {
                                var questionId = worksheet.Cells[row, 1].Value?.ToString().Trim();
                                var questionContent = worksheet.Cells[row, 2].Value?.ToString().Trim();
                                var difficulty = worksheet.Cells[row, 3].Value?.ToString().Trim();

                                if (string.IsNullOrEmpty(questionId) || string.IsNullOrEmpty(questionContent) || string.IsNullOrEmpty(difficulty))
                                {
                                    failedRows.Add($"Dòng {row}: Thiếu thông tin cơ bản");
                                    continue;
                                }

                                // Kiểm tra độ khó hợp lệ
                                if (difficulty != "Easy" && difficulty != "Medium" && difficulty != "Hard")
                                {
                                    failedRows.Add($"Dòng {row}: Độ khó không hợp lệ (phải là Easy, Medium hoặc Hard)");
                                    continue;
                                }

                                // Kiểm tra ID trùng
                                if (_context.Questions.Any(q => q.QuestionId == questionId))
                                {
                                    failedRows.Add($"Dòng {row}: ID câu hỏi '{questionId}' đã tồn tại");
                                    continue;
                                }

                                // Đọc đáp án
                                var answerTexts = new List<string>();
                                var correctAnswers = new List<bool>();
                                int col = 4;

                                while (col < worksheet.Dimension.Columns - 1)
                                {
                                    var answerText = worksheet.Cells[row, col].Value?.ToString().Trim();
                                    var isCorrectStr = worksheet.Cells[row, col + 1].Value?.ToString().Trim();

                                    if (!string.IsNullOrEmpty(answerText) && !string.IsNullOrEmpty(isCorrectStr))
                                    {
                                        answerTexts.Add(answerText);
                                        correctAnswers.Add(isCorrectStr.ToLower() == "true");
                                    }
                                    col += 2;
                                }

                                if (answerTexts.Count == 0)
                                {
                                    failedRows.Add($"Dòng {row}: Không có đáp án");
                                    continue;
                                }

                                if (!correctAnswers.Contains(true))
                                {
                                    failedRows.Add($"Dòng {row}: Không có đáp án đúng");
                                    continue;
                                }

                                // Tạo đối tượng Question
                                var question = new Question
                                {
                                    QuestionId = questionId,
                                    QuestionContent = questionContent,
                                    SubjectId = subjectId,
                                    Difficulty = difficulty
                                };

                                var answers = new List<dynamic>();
                                for (int i = 0; i < answerTexts.Count; i++)
                                {
                                    answers.Add(new
                                    {
                                        id = (i + 1).ToString(),
                                        content = answerTexts[i],
                                        isCorrect = correctAnswers[i]
                                    });
                                }
                                question.AnswersJson = JsonConvert.SerializeObject(answers);

                                importedQuestions.Add(question);
                                successCount++;
                            }
                            catch (Exception ex) // Bắt lỗi cho từng dòng
                            {
                                failedRows.Add($"Dòng {row}: Lỗi dữ liệu - {ex.Message}. StackTrace: {ex.StackTrace}");
                                continue; // Tiếp tục xử lý dòng tiếp theo thay vì thoát hoàn toàn
                            }
                        }
                    }
                }
                // Xử lý CSV nếu cần...

                // Lưu vào DB
                if (importedQuestions.Any())
                {
                    await _context.Questions.AddRangeAsync(importedQuestions);
                    await _context.SaveChangesAsync();
                }

                // Báo cáo kết quả
                if (failedRows.Any())
                {
                    TempData["ErrorMessage"] = $"Import hoàn tất với {successCount} câu hỏi thành công và {failedRows.Count} lỗi. Chi tiết: {string.Join("; ", failedRows)}";
                }
                else
                {
                    TempData["StatusMessage"] = $"Import thành công {successCount} câu hỏi.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) // Bắt lỗi chung (file không mở được, license, v.v.)
            {
                TempData["ErrorMessage"] = $"Lỗi khi xử lý file: {ex.Message}. StackTrace: {ex.StackTrace}";
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
                return View();
            }
        }
    }
}