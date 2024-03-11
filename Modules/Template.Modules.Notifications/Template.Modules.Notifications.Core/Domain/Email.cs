using Template.Modules.Shared.Core.Exceptions;
using Template.Modules.Shared.Core.Exceptions.Codes;

namespace Template.Modules.Notifications.Core.Domain
{
    public class Email
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public string Subject { get; private set; }
        public string Template { get; private set; }

        public Email(string from, string to, string subject, string template)
        {
            SetFrom(from);
            SetTo(to);
            SetSubject(subject);
            SetTemplate(template);
        }

        private void SetFrom(string from)
        {
            if (from.Length > 50)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidLengthCode(nameof(from)),
                    $"Email can have maximum 50 characters.");
            }
            From = from;
        }

        private void SetTo(string to)
        {
            if (to.Length > 50)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidLengthCode(nameof(to)),
                    $"Email can have maximum 50 characters.");
            }
            To = to;
        }

        private void SetSubject(string subject)
        {
            if (subject.Length > 200)
            {
                throw new BusinessException(ExceptionCodes.GetInvalidLengthCode(nameof(subject)),
                    $"Subject can have maximum 200 characters.");
            }
            Subject = subject;
        }

        private void SetTemplate(string template)
        {
            Template = template;
        }
    }
}
