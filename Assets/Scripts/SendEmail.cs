using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class SendEmail : MonoBehaviour
{
    public InputField bodyMessage;
    public InputField subjectMessage;
    public InputField recipientEmail;

    [Tooltip("Use Outlook email id")]
    public InputField senderEmail;
    public InputField Password;

    public void SendEmailButton()
    {
        MailMessage mail = new MailMessage();
        //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com"); 
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;

        mail.From = new MailAddress(senderEmail.text);
        mail.To.Add(new MailAddress(recipientEmail.text));

        mail.Subject = subjectMessage.text;
        mail.Body = bodyMessage.text;

        SmtpServer.Credentials = new System.Net.NetworkCredential(senderEmail.text, Password.text) as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    } 
}
