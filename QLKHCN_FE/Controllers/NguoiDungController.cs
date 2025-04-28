using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QLKHCN_FE.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QLKHCN_FE.Controllers
{
    public class NguoiDungController : BaseController
    {
        private readonly HttpClient _httpClient;

        // GET: NguoiDung
        public NguoiDungController()
        {

            _httpClient = new HttpClient();
            
            /*_httpClient.BaseAddress = new Uri("https://localhost:44364/authentication-v2/api/auth/");
            */
        }
        public ActionResult abc()
        {
            return View();
        }
        public async Task<ActionResult> DeTai()
        {
            if (!IsAuthenticated)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "NguoiDung");
            }

            // var isPasswordNullOrEmpty = await isNullOrEmptyPassword();
            //
            // if (!isPasswordNullOrEmpty)
            // {
                return View();
            // }
            //
            // Session["ReturnUrl"] = Request.Url.AbsoluteUri;
            // return RedirectToAction("SetPassword", "NguoiDung");
        }
        public async Task<ActionResult> DeTaiSinhVien()
        {
            if (!IsAuthenticated)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "NguoiDung");
            }

            // var isPasswordNullOrEmpty = await isNullOrEmptyPassword();
            //
            // if (!isPasswordNullOrEmpty)
            // {
                return View();
            // }
            //
            // Session["ReturnUrl"] = Request.Url.AbsoluteUri;
            // return RedirectToAction("SetPassword", "NguoiDung");
        }

        public ActionResult LyLich()
        {
            if (!IsAuthenticated)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "NguoiDung");
            }
            return View();
        }

        public ActionResult LyLich2()
        {
            if (!IsAuthenticated)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "NguoiDung");
            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("Khoa");
            }
            catch { }
            HttpContext.Session.Remove("CurrentUser");
            return RedirectToAction("Login", "NguoiDung");
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<ActionResult> SetPassword()
        {
            var isPasswordNullOrEmpty = await isNullOrEmptyPassword();

            if (isPasswordNullOrEmpty)
            {
                return View();
            }

            return RedirectToAction("DeTai", "NguoiDung");
        }
        
        [HttpPost]
        public async Task<ActionResult> Token2()
        {
            var stoken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJIb1RlbiI6IlRy4bqnbiBRdeG7kWMgVGhhbyIsIklEVXNlciI6IlRUUTAwMzA4ODAiLCJDaHVjRGFuaCI6IlBow7Mga2hvYSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IjAiLCJqdGkiOiJhOTEzMDkyNi1mZWQ1LTRhZmUtOGQxYi1mOWJjMGE5MjRhMDUiLCJleHAiOjE3MTAyNDczNDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzY0LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzY0LyJ9.APxwI9mmLxZTRTI0muFIosPdzvReb1Thu_Z7eSqbdtY";
            try
            {

                using (var client = new HttpClient())
                {
                    // Build the content
                    var content = new StringContent(stoken, Encoding.UTF8, "application/json");

                    // Make the request
                    var response = await client.PostAsync("https://localhost:44364/api/NguoiDung/login2?request=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJIb1RlbiI6IlRy4bqnbiBRdeG7kWMgVGhhbyIsIklEVXNlciI6IlRUUTAwMzA4ODAiLCJDaHVjRGFuaCI6IlBow7Mga2hvYSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IjAiLCJqdGkiOiJhOTEzMDkyNi1mZWQ1LTRhZmUtOGQxYi1mOWJjMGE5MjRhMDUiLCJleHAiOjE3MTAyNDczNDgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzY0LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzY0LyJ9.APxwI9mmLxZTRTI0muFIosPdzvReb1Thu_Z7eSqbdtY", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        dynamic obj = JsonConvert.DeserializeObject(responseContent);
                        string token = obj.token;

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var authtoken = tokenHandler.ReadJwtToken(token);
                        var claims = authtoken.Claims;
                        var chucdanh = claims.FirstOrDefault(c => c.Type == "ChucDanh")?.Value;

                        string ngach = "GV";

                        if (chucdanh.Contains("NCV"))
                        {
                            ngach = "NCV";
                        }
                        var userlogin = new UserLogin
                        {
                            IDUser = claims.FirstOrDefault(c => c.Type == "IDUser")?.Value,
                            HoTen = claims.FirstOrDefault(c => c.Type == "HoTen")?.Value,
                            Ngach = ngach,
                            Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                            Token = token
                        };
                        HttpContext.Session.Add("CurrentUser", userlogin);

                        if (Session["ReturnUrl"] != null)
                        {
                            string returnUrl = Session["ReturnUrl"].ToString();
                            Session.Remove("ReturnUrl");
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("DeTai", "NguoiDung");
                        }
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        dynamic obj = JsonConvert.DeserializeObject(responseContent);
                        if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
                        {
                            string message = obj.message;
                            ModelState.AddModelError("", message);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đăng nhập thất bại!");
                        }
                        return View();
                    }
                }
            }
            catch { ModelState.AddModelError("", "Lỗi"); }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest request)
        {

            var client = new HttpClient();
            var json1 = JsonConvert.SerializeObject(request);
            var content1 = new StringContent(json1);
            content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("https://localhost:44364/api/NguoiDung/login", content1);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic obj = JsonConvert.DeserializeObject(responseContent);
                string token = obj.token;

                var tokenHandler = new JwtSecurityTokenHandler();
                var authtoken = tokenHandler.ReadJwtToken(token);
                var claims = authtoken.Claims;
                var chucdanh = claims.FirstOrDefault(c => c.Type == "ChucDanh")?.Value;
                
                string ngach = "GV";
                
                if (chucdanh.Contains("NCV"))
                {
                    ngach = "NCV";
                }
                var userlogin = new UserLogin
                {
                    IDUser = claims.FirstOrDefault(c => c.Type == "IDUser")?.Value,
                    HoTen = claims.FirstOrDefault(c => c.Type == "HoTen")?.Value,
                    Ngach = ngach,
                    Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                    Token = token
                };
                HttpContext.Session.Add("CurrentUser", userlogin);

                if (Session["ReturnUrl"] != null)
                {
                    string returnUrl = Session["ReturnUrl"].ToString();
                    Session.Remove("ReturnUrl");
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("DeTai", "NguoiDung");
                }
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic obj = JsonConvert.DeserializeObject(responseContent);
                if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
                {
                    string message = obj.message;
                    ModelState.AddModelError("", message);
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại!");
                }
                return View(request);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Token(string stoken)
        {
            using (var client = new HttpClient())
            {
                // Build the content
                var content = new StringContent(stoken, Encoding.UTF8, "application/json");

                // Make the request
                var response = await client.PostAsync("https://localhost:44364/api/NguoiDung/login2?request=" + stoken, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic obj = JsonConvert.DeserializeObject(responseContent);
                    string token = obj.token;

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var authtoken = tokenHandler.ReadJwtToken(token);
                    var claims = authtoken.Claims;
                    var chucdanh = claims.FirstOrDefault(c => c.Type == "ChucDanh")?.Value;

                    string ngach = "GV";

                    if (chucdanh.Contains("NCV"))
                    {
                        ngach = "NCV";
                    }
                    var userlogin = new UserLogin
                    {
                        IDUser = claims.FirstOrDefault(c => c.Type == "IDUser")?.Value,
                        HoTen = claims.FirstOrDefault(c => c.Type == "HoTen")?.Value,
                        Ngach = ngach,
                        Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                        Token = token
                    };
                    HttpContext.Session.Add("CurrentUser", userlogin);

                    if (Session["ReturnUrl"] != null)
                    {
                        string returnUrl = Session["ReturnUrl"].ToString();
                        Session.Remove("ReturnUrl");
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("DeTai", "NguoiDung");
                    }
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic obj = JsonConvert.DeserializeObject(responseContent);
                    if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
                    {
                        string message = obj.message;
                        ModelState.AddModelError("", message);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng nhập thất bại!");
                    }
                    return View();
                }
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> SetPassword(UpdatePasswordRequest request)
        {
            if (!request.password.Equals(request.confirmPassword))
            {
                ModelState.AddModelError("", "Mật khẩu không khớp!");
                return View();
            }

            using (var client = new HttpClient())
            {
                var user = HttpContext.Session["CurrentUser"] as UserLogin;
                if (user == null || string.IsNullOrEmpty(user.Token))
                {
                    ModelState.AddModelError("", "Không rõ thông tin người dùng, vui lòng đăng nhập lại!");
                    return View();
                }

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);
                var json1 = JsonConvert.SerializeObject(new
                {
                    username = user.IDUser,
                    password = request.password
                });
                var content1 = new StringContent(json1);
                content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PutAsync("https://localhost:44364/api/NguoiDung/set-password-first", content1);

                if (response.IsSuccessStatusCode)
                {
                    if (Session["ReturnUrl"] != null)
                    {
                        string returnUrl = Session["ReturnUrl"].ToString();
                        Session.Remove("ReturnUrl");
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("DeTai", "NguoiDung");
                }
                
                var errorContent = await response.Content.ReadAsStringAsync(); // <<< lấy nội dung lỗi
                ModelState.AddModelError("", $"Đặt mật khẩu thất bại! Chi tiết: {errorContent}");
                return View();
            }
        }


        public ActionResult Role()
        {
            if (!IsAuthenticated)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "NguoiDung");
            }
            return View();
        }

        private async Task<bool> isNullOrEmptyPassword()
        {
            using (var client = new HttpClient())
            {
                var user = HttpContext.Session["CurrentUser"] as UserLogin;
                if (user == null || string.IsNullOrEmpty(user.Token))
                {
                    return true;
                }

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.Token);
                var response = await client.GetAsync("https://localhost:44364/api/NguoiDung/required");

                if (response.IsSuccessStatusCode)
                {
                    return false;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return true; // Nếu 404 thì trả về true
                }
                else
                {
                    return false; // Các lỗi khác thì trả false
                }
            }
        }
    }
}