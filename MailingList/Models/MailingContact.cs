using System;
namespace MailingList.Models
{
    public class MailingContact
    {

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
