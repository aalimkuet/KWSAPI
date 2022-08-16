using KWSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KWS.Models
{
  public interface IKWSDBContext
  {
	DbSet<MemberMaster> MemberMasters { get; set; }
	DbSet<UserAuthen> User { get; set; }
  }
}
