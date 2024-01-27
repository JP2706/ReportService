using Cipher;
using EmailSender;
using ReportService.Core;
using ReportService.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //var stringCipher = new StringCipher("2");
            //var encryptedPassword = stringCipher.Encrypt("hasło");
            //var decryptedPassword = stringCipher.Decrypt(encryptedPassword);

            //Console.WriteLine(encryptedPassword);
            //Console.WriteLine(decryptedPassword);

            //return;

            var emailReceiver = "cos@cos.pl";

            var htmlEmail = new GenerateHtmlEmail();

            var email = new Email(new EmailParams
            {
                HostSmtp = "",
                Port = 1,
                EnableSsl = true,
                SenderName = "",
                SenderEmail = "",
                SenderEmailPassword = ""
            });

            var report = new Report
            {
                Id = 1,
                Title = "R/1/2020",
                Date = new DateTime(2020, 1, 1, 12, 0, 0),
                Positions = new List<ReportPosition>
                {
                    new ReportPosition
                    {
                        Id = 1,
                        ReportId = 1,
                        Title = "Position 1",
                        Description = "Description 1",
                        Value = 43.01m
                    },
                    new ReportPosition
                    {
                        Id = 2,
                        ReportId = 1,
                        Title = "Position 2",
                        Description = "Description 2",
                        Value = 4311m
                    },
                    new ReportPosition
                    {
                        Id = 3,
                        ReportId = 1,
                        Title = "Position 3",
                        Description = "Description 3",
                        Value = 1.99m
                    }
                }
            };

            var errors = new List<Error>
            {
                new Error { Message = "Błąd testowy 1", Date = DateTime.Now },
                new Error { Message = "Błąd testowy 2", Date = DateTime.Now }
            };

            Console.WriteLine("Wysyłanie e-mail (Raport dobowy)...");

            email.Send("Raport dobowy", htmlEmail.GenerateReport(report), emailReceiver).Wait();

            Console.WriteLine("Wysłano e-mail (Raport dobowy)...");

            Console.WriteLine("Wysyłanie e-mail (Błędy w aplikacji)...");

            email.Send("Błędy w aplikacji", htmlEmail.GenerateErrors(errors, 10), emailReceiver).Wait();

            Console.WriteLine("Wysłano e-mail (Błędy w aplikacji)...");

        }
    }
}
