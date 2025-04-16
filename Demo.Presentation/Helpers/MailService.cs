using Demo.Presentation.Settings;
using Demo.Presentation.Utilities;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Demo.Presentation.Helpers
{
    public class MailService : IMailService
    {
        private readonly IOptions<MailSettings> _options;

        public MailService(IOptions<MailSettings> options)
        {
            _options = options;
        }
        public void Send(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Value.Email),
                Subject = _options.Value.Email,
            };
        }
    }
}
