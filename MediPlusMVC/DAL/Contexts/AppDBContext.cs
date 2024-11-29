using Microsoft.EntityFrameworkCore;

namespace MediPlusMVC.DAL.Contexts
{
	public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions options) : base(options) { }
	}
}
