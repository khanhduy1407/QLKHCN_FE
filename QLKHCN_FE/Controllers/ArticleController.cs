using System.IO;
using System;
using System.Net.Mail;
using System.Web.Mvc;
using System.Collections.Generic;

namespace QLKHCN_FE.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: Article
        public ActionResult Index()
        {
            if (!IsAuthenticated)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "NguoiDung");
            }
            return View();
        }

        public ActionResult SendMail()
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ngluan161121@gmail.com", "xujblbzyltgzpqbk");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("ngluan161121@gmail.com");
            mail.To.Add("taikhoanrac14789@gmail.com");
            mail.Subject = "Thông báo xác nhận";
            mail.IsBodyHtml = true;
            mail.Body = "Bạn đã được thêm vào danh sách tác giả bài báo";
            mail.Priority = MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ngluan161121@gmail.com", "xujblbzyltgzpqbk");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            return RedirectToAction("DeTai", "NguoiDung");
        }

        [HttpPost]
        public ActionResult SaveFile(string fileName, string fileData, string userId, string valuetenbaibao)
        {
            try
            {
                // Giải mã dữ liệu base64
                var base64Data = fileData;
                var bytes = Convert.FromBase64String(base64Data);

                // Đường dẫn thư mục lưu trữ tệp tin
                var uploadDirectory = Server.MapPath($"~/Uploads/{userId}/{valuetenbaibao}/");

                // Tạo thư mục lưu trữ nếu chưa tồn tại
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Đường dẫn tới tệp tin mới
                var targetPath = Path.Combine(uploadDirectory, fileName);

                // Ghi dữ liệu vào tệp tin
                System.IO.File.WriteAllBytes(targetPath, bytes);

                // Trả về kết quả thành công
                return Json(new { success = true, filePath = "/Uploads/" + userId + "/" + valuetenbaibao + "/" + fileName });
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ khi lưu trữ tệp tin thất bại
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult DeleteFilesInDirectory(string directoryName)
        {
            try
            {

                var basePath = Server.MapPath("~/Uploads");
                var directoryPath = Path.Combine(basePath, directoryName);


                if (Directory.Exists(directoryPath))
                {

                    DirectoryInfo directory = new DirectoryInfo(directoryPath);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        file.Delete();
                    }
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult DeleteFile(string fileUrl)
        {
            try
            {
                // Giải mã đường dẫn file
                string decodedFileUrl = Server.MapPath(fileUrl);

                // Kiểm tra file có tồn tại không
                if (System.IO.File.Exists(decodedFileUrl))
                {
                    // Xóa tệp tin
                    System.IO.File.Delete(decodedFileUrl);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Tệp không tồn tại." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}