using Microsoft.EntityFrameworkCore;

namespace MailingList.Models
{
    public class MailingContext : DbContext
    {

        public MailingContext(DbContextOptions<MailingContext> options)
            : base(options)
        {
        }

        public DbSet<MailingContact> MailingContacts { get; set; }

    }
}
