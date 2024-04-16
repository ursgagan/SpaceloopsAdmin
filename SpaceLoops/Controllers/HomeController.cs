using Microsoft.AspNetCore.Mvc;
using SpaceLoops.BAL.Services;
using SpaceLoops.DAL.Entity;
using SpaceLoops.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Runtime;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;


namespace SpaceLoops.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContactServices _contactServices;
        private readonly IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, ContactServices contactServices, IConfiguration configuration)
        {
            _logger = logger;
            _contactServices = contactServices;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.PageName = "Index";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ContactUs()
        {
            // await SendContactEmail("Gagan.spaceloops@gmail.com", "Gagan", "Testing Message", "9815557158");
            
            //await SendAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(Contact contact)
        {
            try
            {
                if (contact.Id == null || contact.Id == Guid.Empty)
                {
                    await _contactServices.AddContact(contact);
                    TempData["SuccessMessage"] = "Contacts Added Successfully";

                    await SendAsync(contact.EmailId, contact.FirstName, contact.Message, contact.PhoneNumber);


                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again later.";
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }

        private async Task<bool> SendAsync(string userEmail, string userName, string Contactmessage, string contactNumber, CancellationToken ct = default)
        {
            try
            {
                string fileName = "NewUserAdded.html";
                string folderPath = @"wwwroot\EmailTemplates\";

                string emailBody = System.IO.File.ReadAllText(Path.Combine(folderPath, fileName));

                emailBody = emailBody.Replace("[UserName]", userName);
                emailBody = emailBody.Replace("[UserEmail]", userEmail);
                emailBody = emailBody.Replace("[Message]", Contactmessage);
                emailBody = emailBody.Replace("[PhoneNumber]", contactNumber);

                // Initialize a new instance of the MimeKit.MimeMessage class
                var mail = new MimeMessage();

                bool useSSL = true;
                #region Sender / Receiver
                // Sender
                mail.From.Add(new MailboxAddress("info@spaceloops.co.in", "info@spaceloops.co.in"));
                mail.Sender = new MailboxAddress("info@spaceloops.co.in", "info@spaceloops.co.in");


                // Receiver
                //foreach(string mailAddress in recieverMailAddress)
                mail.To.Add(MailboxAddress.Parse("gagan.spaceloops@gmail.com"));


                // Set Reply to if specified in mail data
                //if (!string.IsNullOrEmpty(mailData.ReplyTo))
                //    mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

                // BCC
                // Check if a BCC was supplied in the request
                //if (mailData.Bcc != null)
                //{
                //    // Get only addresses where value is not null or with whitespace. x = value of address
                //    foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                //        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                //}

                // CC
                // Check if a CC address was supplied in the request
                //if (mailData.Cc != null)
                //{
                //    foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                //        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                //}
                #endregion

                #region Content

                // Add Content to Mime Message
                var body = new BodyBuilder();
                mail.Subject = "New Contact Added";
                body.HtmlBody = emailBody;
                mail.Body = body.ToMessageBody();


                #endregion

                #region Send Mail

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                if (useSSL)
                {
                    await smtp.ConnectAsync("smtpout.secureserver.net.", 465, SecureSocketOptions.SslOnConnect, ct);
                }
                //else if (_settings.UseStartTls)
                //{
                //    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
                //}
                await smtp.AuthenticateAsync("info@spaceloops.co.in", "Loops*First", ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);

                #endregion

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
