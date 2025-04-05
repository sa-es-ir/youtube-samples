namespace DeepDiveToDI.Domain;

public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}


public class GoogleEmailService : IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email via Google: To: {to}, Subject: {subject}, Body: {body}");
    }
}


public class YahooEmailService : IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email via Yahoo: To: {to}, Subject: {subject}, Body: {body}");
    }
}
