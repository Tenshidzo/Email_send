using System;
using System.Net;
using System.Net.Mail;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите ваш адрес электронной почты (отправитель):");
        string senderEmail = Console.ReadLine();

        Console.WriteLine("Введите пароль к вашему email:");
        string password = Console.ReadLine();

        Console.WriteLine("Введите адрес электронной почты получателя:");
        string recipientEmail = Console.ReadLine();

        Console.WriteLine("Введите тему письма:");
        string subject = Console.ReadLine();

        Console.WriteLine("Введите текст письма:");
        string body = Console.ReadLine();

        Console.WriteLine("Введите путь к файлу для прикрепления (или оставьте пустым, если файл не требуется):");
        string attachmentPath = Console.ReadLine();

        try
        {
            SendEmail(senderEmail, password, recipientEmail, subject, body, attachmentPath);
            Console.WriteLine("Письмо успешно отправлено!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при отправке письма: {ex.Message}");
        }
    }

    static void SendEmail(string senderEmail, string password, string recipientEmail, string subject, string body, string attachmentPath)
    {
        // Настройки SMTP клиента (используем Gmail в качестве примера)
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(senderEmail, password),
            EnableSsl = true
        };

        // Создание сообщения
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = false // Можно установить true, если требуется HTML-сообщение
        };

        // Добавляем получателя
        mailMessage.To.Add(recipientEmail);

        // Добавление вложения, если указано
        if (!string.IsNullOrWhiteSpace(attachmentPath) && File.Exists(attachmentPath))
        {
            Attachment attachment = new Attachment(attachmentPath);
            mailMessage.Attachments.Add(attachment);
        }

        // Отправка письма
        smtpClient.Send(mailMessage);
    }
}
