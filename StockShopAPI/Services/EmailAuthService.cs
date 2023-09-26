using System;
using System.Net;
using System.Net.Mail;
using StockShopAPI.Helpers;

namespace StockShopAPI.Repositories
{
    public class EmailAuthService
    {
        private DataContext _context;
        public EmailAuthService(DataContext context)
        {
            _context = context;
        }

        public async Task SendConfirmSignup(string recipientEmail, string username, string confirmationLink)
        {
            var senderEmail = "stockshopservice@gmail.com";
            var senderPassword = "Mateusz.18";
            var smtpServer = "localhost"; // Replace with your SMTP server address
            var smtpPort = 1025; // Replace with the appropriate SMTP port

            // Create an SMTP client with the server and port information
            var client = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = false, // Enable SSL if your SMTP server requires it
            };

            // Create an email message
            var message = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Confirm Your Account",
                Body = $@"
                    <!DOCTYPE html>
<html>

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f7f7f7; /* Background color */
            margin: 0;
            padding: 0;
            text-align: center;
        }}

        h1 {{
            color: #000; /* Header color */
            font-size: 24px;
            margin-top: 20px;
        }}

        p {{
            color: #333; /* Text color */
            font-size: 16px;
            margin: 10px 0;
        }}

        a {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #007BFF; /* Button background color */
            color: #fff; /* Button text color */
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
            transition: background-color 0.3s ease;
        }}

        a:hover {{
            background-color: #0056b3; /* Button hover background color */
        }}
    </style>
</head>

<body>
    <h1>Confirm Your Account</h1>
    <p>Hello {{username}},</p>
    <p>Thank you for signing up for our online store. To complete your registration, please click the button below to confirm your account:</p>
    <p>
        <a href=""{{confirmationLink}}"">Confirm My Account</a>
    </p>
    <p>If you didn't sign up for our store, you can ignore this email.</p>
    <p>Thank you!</p>
</body>

</html>
",
                IsBodyHtml = true, // Set to true since the email body is in HTML format
            };

            // Add the recipient's email address
            message.To.Add(recipientEmail);

            try
            {
                // Send the email
                await client.SendMailAsync(message);
                Console.WriteLine("Confirmation email sent successfully!");
            }
            catch (SmtpFailedRecipientsException recipientsEx)
            {
                Console.WriteLine($"SMTP error: Failed recipients - {recipientsEx.Message}");
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP error: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending confirmation email: {ex.Message}");
            }
            finally
            {
                // Dispose of the SMTP client to release resources
                client.Dispose();
            }

        }

    }
}

