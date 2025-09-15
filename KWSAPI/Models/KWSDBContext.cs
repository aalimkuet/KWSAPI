using KWSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KWS.Models
{
    public class KWSDBContext : DbContext, IKWSDBContext
    {
        public KWSDBContext(DbContextOptions<KWSDBContext> options) : base(options)
        {

        }
        public DbSet<MemberMaster> MemberMasters { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
