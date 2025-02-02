﻿using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage(); // MimeMessage nesnesi oluşturdum (e-posta yapısı için)

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "traversalcore2@gmail.com"); // Gönderen e-posta adresi ve adı belirleme Standart Mail bu 

            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = mailRequest.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("traversalcore2@gmail.com", "fhvevuwmjwlkpnzm"); // SMTP kimlik doğrulaması yapma Buna bir daha bak (Gmail hesap bilgileri)
            client.Send(mimeMessage);
            client.Disconnect(true);
            return View();
        }
    }
}
//traversalcore2@gmail.com