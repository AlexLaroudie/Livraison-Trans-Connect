using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Dans cette classe on va chercher à pouvoir envoyer par mail les factures au client
    /// On utilise pour cela  smtp sur la base de gmail
    /// </summary>
    internal class Mailing
    {
        public static string GenererPDF(string fileName, string content)
        {
            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

           
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
            document.Open();

            
            document.Add(new Paragraph(content));

            
            document.Close();

            return pdfPath;
        }

        public static void EnvoyerMail(string destinataire, string subject, string body, string attachmentPath)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("laroudiealexandre@gmail.com");
                mail.To.Add(destinataire);
                mail.Subject = subject;
                mail.Body = body;

               
                Attachment attachment = new Attachment(attachmentPath);
                mail.Attachments.Add(attachment); 

                
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential("laroudiealexandre@gmail.com", "iolr lwga iyol btxn");
                    smtp.EnableSsl = true;

                    
                    smtp.Send(mail);
                }
            }
        }
    }
}
