using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System.Net.Mail;
using System.Net;

namespace Movie.WEBUI.Controllers
{
	public class ContactController : Controller
	{
        private readonly IGenericService<ContactDto, Contact> _service;
        public ContactController(IGenericService<ContactDto, Contact> service)
        {
            _service = service;
        }
        public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create(ContactDto itemDto)
        {

            if (ModelState.IsValid)
            {
            //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            //    var configuration = builder.Build();
            //    var host = configuration["Gmail:Host"];
            //    var port = int.Parse(configuration["Gmail:Port"]);
            //    var username = configuration["Gmail:Username"];
            //    var password = configuration["Gmail:Password"];
            //    var displayName = configuration["Gmail:DisplayName"];

            //    var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

            //    MailMessage mail = new MailMessage($"{itemDto.Email}", $"{username}");
            //    mail.Subject = displayName;


            //    mail.Body = $@"
            //   <html>
            //   <head> 
            //   <style>
              

            //  </style>
            //  </head>
            //  <body>
            //  <h2>Name: {itemDto.FullName} </h2>
            //  <h3>Email: {itemDto.Email}</h2> 
            //   <h3>Message <br /> {itemDto.Message}</h3>
            //</body>
            //</html>";
            //    mail.IsBodyHtml = true;
            //    SmtpClient smtpClient = new SmtpClient(host, port);
            //    smtpClient.Credentials = new NetworkCredential(username, password);
            //    smtpClient.EnableSsl = true;
            //    smtpClient.Send(mail);

                ViewBag.msg = "Sizin müraciətiniz uğurla göndərildi!";

                var category = await _service.AddAsync(itemDto);

                if (category != null)
                {
                    return RedirectToAction("Create");
                }

                _service.AddAsync(itemDto);
            }
            return View(itemDto);
        }
    }
}
