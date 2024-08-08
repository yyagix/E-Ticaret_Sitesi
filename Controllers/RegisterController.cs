using Microsoft.AspNetCore.Mvc;
using ProductApp.Dto;
using ProductApp.Models;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MailKit.Net.Smtp;

namespace ProductApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000);
            AppUser appUser = new AppUser()
            {
                FirstName = appUserRegisterDto.FirstName,
                LastName = appUserRegisterDto.LastName,
                City = appUserRegisterDto.City,
                UserName = appUserRegisterDto.UserName,
                Email = appUserRegisterDto.Email,
                ConfirmedCode = code
            };

            var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
            if (result.Succeeded)
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("E-Ticaret Uygulaması", "yagizefeunal81@gmail.com");
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);
                mimeMessage.From.Add(mailboxAddressFrom);
                mimeMessage.To.Add(mailboxAddressTo);
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = $"Kaydınız Başarılı Şekilde Gerçekleşti. Onay Kodu: {code}";
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "E-Ticaret Uygulaması";
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("yagizefeunal81@gmail.com", "ztcp sore yvrm qgyr");
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }

                TempData["Mail"] = appUserRegisterDto.Email; 
                return RedirectToAction("Index", "ConfirmMail");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(appUserRegisterDto);
        }
    }
}
