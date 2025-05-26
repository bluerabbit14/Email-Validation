using DnsClient;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace EmailValidationAPI.Services
{
    public class EmailValidator
    {
        private readonly LookupClient _dnsClient;

        public EmailValidator()
        {
            _dnsClient = new LookupClient();
        }

        public bool IsValidFormat(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public bool HasMXRecord(string domain)
        {
            try
            {
                var result = _dnsClient.Query(domain, QueryType.MX);
                return result.Answers.MxRecords().Any();
            }
            catch
            {
                return false;
            }
        }

        public bool IsEmailDeliverable(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return HasMXRecord(address.Host);
            }
            catch
            {
                return false;
            }
        }
    }
}