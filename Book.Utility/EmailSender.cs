using Microsoft.AspNetCore.Identity.UI.Services;

namespace Book.Utility;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        //logik to send email
        return Task.CompletedTask;
    }
}
