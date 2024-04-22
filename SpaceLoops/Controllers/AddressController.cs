using Abp.Extensions;
using Microsoft.AspNetCore.Mvc;
using SpaceLoops.BAL.Services;
using SpaceLoops.DAL.Entities;
using SpaceLoops.DAL.Entity;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using System.Web;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SpaceLoops.Helpers;
using static System.Net.Mime.MediaTypeNames;
using Image = SpaceLoops.DAL.Entities.Image;
using Microsoft.AspNetCore.Hosting.Server;
using NUglify.JavaScript.Syntax;
using SpaceLoops.DAL.Model;


namespace SpaceLoops.Controllers
{
    public class AddressController : Controller
    {
        private readonly ILogger<AddressController> _logger;
        private readonly AddressServices _addressServices;
        private readonly StateServices _stateServices;
        private readonly CityServices _cityServices;
        private readonly UserRegistrationService _userRegistrationServices;
        private readonly ImageServices _imageServices;
        public AddressController(ILogger<AddressController> logger, AddressServices addressServices, StateServices stateServices, CityServices cityServices, UserRegistrationService userRegistrationServices, ImageServices imageServices)
        {
            _logger = logger;
            _addressServices = addressServices;
            _stateServices = stateServices;
            _cityServices = cityServices;
            _userRegistrationServices = userRegistrationServices;
            _imageServices = imageServices;

        }
        public IActionResult Index()
        {
            return View();
        }


        #region Country

        public IActionResult AddCountry(string countryId)
        {
            Country country = new Country();

            if (countryId != null)
            {
                var cId = new Guid(countryId);
                country = _addressServices.GetCountryById(cId);
            }

            return View(country);
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(Country country)
        {
            if (country.Id == null || country.Id == Guid.Empty)
            {
                await _addressServices.AddCountry(country);
                TempData["SuccessMessage"] = "Country Added Successfully";
            }
            else
            {
                _addressServices.UpdateCountry(country);
                TempData["UpdateMessage"] = "Country Updated Successfully";

            }
            Country country1 = new Country();

            return View(country1);
        }


        public IActionResult CountryList()
        {
            ViewBag.CountryList = _addressServices.GetAllCountries();
            return View();
        }

        public IActionResult DeleteCountry(Guid countryId)
        {
            _addressServices.DeleteCountry(countryId);
            TempData["DeleteMessage"] = "Country Deleted Successfully";
            return RedirectToAction("CountryList", "Address");
        }
        #endregion

        #region  State
        public IActionResult AddState(string stateId)
        {
            State state = new State();
            if (stateId != null)
            {
                var sId = new Guid(stateId);
                state = _stateServices.GetStateById(sId);
            }

            ViewBag.CountryList = _addressServices.GetAllCountries();
            return View(state);
        }

        [HttpPost]
        public async Task<IActionResult> AddState(State state)
        {
            if (state.Id == null || state.Id == Guid.Empty)
            {
                await _stateServices.AddState(state);
                ViewBag.CountryList = _addressServices.GetAllCountries();
                TempData["SuccessMessage"] = "State Added Successfully";
            }
            else
            {
                _stateServices.UpdateState(state);
                ViewBag.CountryList = _addressServices.GetAllCountries();
                TempData["UpdateMessage"] = "State Updated Successfully";

            }
            State state1 = new State();

            return View(state1);
        }
        public IActionResult StateList()
        {

            ViewBag.StateList = _stateServices.GetStatesAll();
            return View();
        }


        public IActionResult DeleteState(Guid stateId)
        {
            _stateServices.DeleteState(stateId);
            TempData["DeleteMessage"] = "State Deleted Successfully";
            return RedirectToAction("StateList", "Address");
        }
        public IActionResult UpdateState(Guid stateId)
        {
            State state = _stateServices.GetStateById(stateId);
            return View(state);
        }

        [HttpPost]
        public IActionResult UpdateState(State state)
        {
            _stateServices.UpdateState(state);
            TempData["SuccessMessage"] = "State Edited Successfully";
            return RedirectToAction("StateList", "Address");
        }

        [HttpGet]
        public IActionResult GetStatesByCountryId(Guid countryId)
        {
            var states = _stateServices.GetStatesByCountryId(countryId);
            return Json(states); // Assuming you want to return JSON data
        }

        #endregion

        #region city

        public IActionResult AddCity(string cityId)
        {
            City city = new City();
            if (cityId != null)
            {
                var sId = new Guid(cityId);
                city = _cityServices.GetCityById(sId);
                var stateData = _stateServices.GetStateById(city.StateId);
                ViewBag.CountryId = stateData.CountryId;
            }

            ViewBag.CountryList = _addressServices.GetAllCountries();
            //  ViewBag.StateList = (city.CountryId != Guid.Empty) ? _stateServices.GetStateById(city.CountryId) : new List<State>();
            ViewBag.StateList = _stateServices.GetAllStates();
            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(City city)
        {
            ViewBag.CountryList = _addressServices.GetAllCountries();

            if (city.Id == null || city.Id == Guid.Empty)
            {
                await _cityServices.AddCity(city);
                TempData["SuccessMessage"] = "City Added Successfully";
            }
            else
            {
                _cityServices.UpdateCity(city);
                TempData["UpdateMessage"] = "City Updated Successfully";
            }
            City city1 = new City();

            return View(city1);
        }
        public IActionResult CityList()
        {
            ViewBag.CityList = _cityServices.GetCitiesAll();
            return View();
        }

        public IActionResult DeleteCity(Guid cityId)
        {
            _cityServices.DeleteCity(cityId);
            TempData["DeleteMessage"] = "City Deleted Successfully";
            return RedirectToAction("CityList", "Address");
        }
        public IActionResult UpdateCity(Guid cityId)
        {
            City city = _cityServices.GetCityById(cityId);
            return View(city);
        }

        [HttpPost]
        public IActionResult UpdateCity(City city)
        {
            _cityServices.UpdateCity(city);
            TempData["SuccessMessage"] = "City Edited Successfully";
            return RedirectToAction("CityList", "Address");
        }

        [HttpGet]
        public IActionResult GetCitiesByStatesId(Guid stateId)
        {
            var cities = _userRegistrationServices.GetCitiesByStatesId(stateId);
            return Json(cities); // Assuming you want to return JSON data
        }

        #endregion

        #region  User

        public IActionResult UserRegistration(string userId)
        {
            UserRegistration user = new UserRegistration();
            if (userId != null)
            {
                var sId = new Guid(userId);
                user = _userRegistrationServices.GetUserById(sId);
                ViewBag.CityId = user.CityId;
                var cityData = _cityServices.GetCityById(user.CityId);
                ViewBag.StateId = cityData.StateId;

                var stateData = _stateServices.GetStateById(cityData.StateId);

                ViewBag.CountryId = stateData.CountryId;
            }
            ViewBag.CountryList = _addressServices.GetAllCountries();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserRegistration(UserRegistration user)
        {
            ViewBag.CountryList = _addressServices.GetAllCountries();

            var existingUser = _userRegistrationServices.isUserExist(user.EmailId);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                TempData["AlreadyExistMessage"] = "User with same EmailId Already Exists";
                return RedirectToAction("UserRegistration", "Address", new { userId = user.Id });
            }
            else
            {
                if (user.Id == null || user.Id == Guid.Empty)
                {
                    var generatedOTP = GenerateOTP();
                    user.ResetCode = generatedOTP;
                    await _userRegistrationServices.AddUser(user);
                    TempData["SuccessMessage"] = "User Added Successfully";

                    await SendLoginEmail(user.EmailId, user.FirstName, generatedOTP);
                }
                else
                {
                    _userRegistrationServices.UpdateUser(user);

                    TempData["UpdateMessage"] = "User Updated Successfully";
                }
            }
            UserRegistration user1 = new UserRegistration();

            return RedirectToAction("UserList", "Address");
        }

        [Authorize]
        public IActionResult UserList()
        {
            ViewBag.UserList = _userRegistrationServices.GetUserAll();
            return View();
        }


        public IActionResult DeleteUser(Guid userId)
        {
            _userRegistrationServices.DeleteUser(userId);
            TempData["DeleteMessage"] = "User Deleted Successfully";
            return RedirectToAction("UserList", "Address");
        }
        public IActionResult UpdateUser(Guid userId)
        {
            UserRegistration user = _userRegistrationServices.GetUserById(userId);
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateUser(UserRegistration user)
        {
            _userRegistrationServices.UpdateUser(user);
            TempData["SuccessMessage"] = "User Edited Successfully";
            return RedirectToAction("UserList", "Address");
        }

        #endregion

        #region UserLogin

        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserRegistration userLogin)
        {
            {
                var user = _userRegistrationServices.isUserExist(userLogin.EmailId);

                if (user != null)
                {

                    if (PasswordHasher.VerifyPassword(userLogin.Password, user.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.FirstName),
                            new Claim(ClaimTypes.Email, user.EmailId),
                        };

                        var userIdentity = new ClaimsIdentity("Custom");
                        userIdentity.AddClaims(claims);

                        var principal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), new AuthenticationProperties() { IsPersistent = true });

                        TempData["SuccessMessage"] = "Login Successfully";
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        TempData["PasswordIncorrectMessage"] = "Login Failed";
                        return View();

                    }
                }
                else
                {
                    return View();
                }

            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> UserLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("UserLogin");
        }

        private async Task SendLoginEmail(string userEmail, string userName, string generatedOTP)
        {
            try
            {
                var forgotPasswordURL = "https://localhost:44324/Address/ForgotPassword?resetCode=" + generatedOTP;

                //string emailBody = System.IO.File.ReadAllText(Path.Combine(folderPath, fileName));

                string folderPath = @"wwwroot\EmailTemplates\";

                // Specify the name of the HTML file
                string fileName = "WelcomTemplate.html";

                string filePath = @"EmailTemplates";


                string emailBody = System.IO.File.ReadAllText(Path.Combine(folderPath, fileName));

                emailBody = emailBody.Replace("[UserName]", userName);
                emailBody = emailBody.Replace("[ForgotPasswordURL]", forgotPasswordURL);

                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("Gagan.Spaceloops@gmail.com", "lmje eyix rzkp uubp");
                    client.EnableSsl = true;
                    client.Port = 587;

                    var message = new MailMessage
                    {
                        From = new MailAddress("gagan.spaceloops@gmail.com"),

                        Subject = "SignUp Successful",
                        Body = emailBody,
                        IsBodyHtml = true,
                    };

                    message.To.Add(userEmail);
                    message.To.Add("Gagan.Spaceloops@gmail.com");

                    await client.SendMailAsync(message);

                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or take appropriate action
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        #endregion

        #region Password

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string emailId)
        {
            try
            {
                string folderPath = @"wwwroot\EmailTemplates\";

                // Specify the name of the HTML file
                string fileName = "ResetPassword.html";

                var existingUser = _userRegistrationServices.isUserExist(emailId);

                if (existingUser != null)
                {
                    var generatedOTP = GenerateOTP();
                    existingUser.ResetCode = generatedOTP;

                    _userRegistrationServices.UpdateUser(existingUser);

                    string firstName = existingUser.FirstName;
                    var forgotPasswordURL = "https://localhost:44324/Address/ForgotPassword?resetCode=" + generatedOTP;

                    string emailBody = System.IO.File.ReadAllText(Path.Combine(folderPath, fileName));

                    emailBody = emailBody.Replace("[UserName]", firstName).Replace("[ForgotPasswordURL]", forgotPasswordURL); ;


                    using (var client = new SmtpClient("smtp.gmail.com"))
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("Gagan.Spaceloops@gmail.com", "lmje eyix rzkp uubp");
                        client.EnableSsl = true;
                        client.Port = 587;

                        var message = new MailMessage
                        {
                            From = new MailAddress("gagan.spaceloops@gmail.com"),

                            Subject = "Forgotten Password",
                            Body = emailBody,
                            IsBodyHtml = true,
                        };

                        message.To.Add(emailId);
                        message.To.Add("Gagan.Spaceloops@gmail.com");

                        await client.SendMailAsync(message);
                        return Json(true);
                    }
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                return Json(false);
            }

        }

        private string GenerateOTP()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        [HttpGet]
        public IActionResult ForgotPassword(string resetCode)
        {
            ViewBag.ResetCode = resetCode;
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string password, string resetCodeId)
        {
            var getUserByResetCode = _userRegistrationServices.GetUserByResetCode(resetCodeId);
            if (getUserByResetCode != null)
            {
                getUserByResetCode.ResetCode = null;
                getUserByResetCode.Password = PasswordHasher.HashPassword(password);
                _userRegistrationServices.UpdateUser(getUserByResetCode);

                TempData["PasswordResetMessage"] = "Login Successfully";
                return Json(true);
            }

            return Json(false);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult CheckPassword()
        {
            return View();
        }

        [Authorize]

        [HttpPost]
        public IActionResult CheckPassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var user = GetLoggerUserData();

            if (user != null)
            {
                //if (PasswordHasher.VerifyPassword(currentPassword, user.Password))
                //{
                //    TempData["PasswordChangedMessage"] = "Password Changed Successfully";
                //    return Json(true);
                //}

                if (newPassword == confirmPassword && PasswordHasher.VerifyPassword(currentPassword, user.Password))
                {
                    user.Password = PasswordHasher.HashPassword(newPassword);
                    _userRegistrationServices.UpdateUser(user);

                   
                    return Json(true);

                }

                else
                {             
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }
        }

        #endregion

        #region Image

        public IActionResult AddImage()
        {
            Image image = new Image();
            return View(image);

        }


        [HttpPost]
        public async Task<IActionResult> AddImage(Image image)
        {
            await _imageServices.AddImage(image);
            TempData["SuccessMessage"] = "Image Added Successfully";


            Image image2 = new Image();

            return View(image2);
        }

        #endregion

        #region User Profile
        public IActionResult EditProfile()
        {
            var user = GetLoggerUserData();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserRegistration userRegistration)
        {
            try
            {
                var user = _userRegistrationServices.isUserExist(userRegistration.EmailId);

                if (user != null)
                {
                    user.FirstName = userRegistration.FirstName;
                    user.LastName = userRegistration.LastName;
                    user.PhoneNumber = userRegistration.PhoneNumber;

                   _userRegistrationServices.UpdateUser(user);

                    return Json(true);
                }
                return Json(false);
            }

            catch (Exception ex)
            {

                return Json(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                Image image = new Image();
                using (var ms = new MemoryStream())
                {
                    await imageFile.CopyToAsync(ms);
                    var imageBytes = ms.ToArray();
                    // Convert byte array to base64 string
                    var base64String = Convert.ToBase64String(imageBytes);

                    image.ImageName = fileName;
                    image.ImageData = base64String;
                    image.CreatedOn = DateTime.Now;
                    image.UpdatedOn = DateTime.Now;
                    image.IsDeleted = false;
                }

                var userClaims = ((ClaimsIdentity)User.Identity).Claims;

                // Find the claim with the email address
                var emailClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email");

                if (emailClaim != null)
                {
                    var emailAddress = emailClaim.Value;

                    var user = _userRegistrationServices.isUserExist(emailAddress);

                    if (user.ImageId != null && (Guid)user.ImageId != Guid.Empty)
                    {
                        image.Id = (Guid)user.ImageId;
                        var imageData = _imageServices.UpdateImage(image);

                        user.ImageId = image.Id;
                        _userRegistrationServices.UpdateUser(user);
                    }
                    else
                    {
                        var imageData = await _imageServices.AddImage(image);
                        user.ImageId = imageData.Id;
                        _userRegistrationServices.UpdateUser(user);
                    }
                    return Json(true);
                    // You can save the base64 string or perform other operations here
                }
            }
            else
            {
                return Json(false);
            }
            return Json(false);
        }

        private UserRegistration GetLoggerUserData()
        {
            UserRegistration user = new UserRegistration();
            var userClaims = ((ClaimsIdentity)User.Identity).Claims;

            // Find the claim with the email address
            var emailClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email");

            if (emailClaim != null)
            {
                var emailAddress = emailClaim.Value;

                user = _userRegistrationServices.isUserExist(emailAddress);

                if (user != null)
                {
                    if (user.ImageId != Guid.Empty && user.ImageId != null)
                    {
                        var imageData = _imageServices.GetImageById((Guid)user.ImageId);

                        ViewBag.ImageData = imageData.ImageData;
                    }
                   
                    return user;
                }
            }
            return user;
        }

        public IActionResult GetUserData()
        {
            var getUserData = GetLoggedUser();
         
                return Json(getUserData);
        }

        private SpaceLoops.Models.UserModel GetLoggedUser()
        {
            SpaceLoops.Models.UserModel userModel = new SpaceLoops.Models.UserModel();
            UserRegistration user = new UserRegistration();
            var userClaims = ((ClaimsIdentity)User.Identity).Claims;

            // Find the claim with the email address
            var emailClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email");

            if (emailClaim != null)
            {
                var emailAddress = emailClaim.Value;

                user = _userRegistrationServices.isUserExist(emailAddress);

                if (user != null)
                {
                    if(user.ImageId != Guid.Empty && user.ImageId != null)
                    {
                        var imageData = _imageServices.GetImageById((Guid)user.ImageId);
                        userModel.ImageId =  imageData.Id;
                        userModel.ImageData = imageData.ImageData;
                        userModel.ImageName = imageData.ImageName;

                        ViewBag.ImageData = imageData.ImageData;
                    }
                    userModel.Id = user.Id;
                    userModel.FirstName = user.FirstName;
                    userModel.LastName = user.LastName;
                    userModel.EmailId = user.EmailId;
                    userModel.PhoneNumber = user.PhoneNumber;
                    userModel.CityId = user.CityId;
                    userModel.IsDeleted = user.IsDeleted;
                    userModel.IsActive = user.IsActive;
                    userModel.CreatedOn = user.CreatedOn;
                    userModel.UpdatedOn = user.UpdatedOn;
                    userModel.ResetCode = user.ResetCode;
        
                    userModel.Password = user.Password;

                   
                    return userModel;
                }
            }
            return userModel;
        }

        #endregion

        public IActionResult GetAllCount()
        
        
        {
          var getCountryCount = _addressServices.GetAllCountries().Count();
            var getStateCount = _stateServices.GetStatesAll().Count();
            var getCityCount = _cityServices.GetCitiesAll().Count();
            var getUserCount = _userRegistrationServices.GetUserAll().Count();

            var count = new
            {
                CountryCount = getCountryCount,
                StateCount = getStateCount,
                CityCount = getCityCount,
                UserCount = getUserCount
            };
            return Json(count);
        }


    }
}