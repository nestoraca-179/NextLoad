using System.Linq;
using System.Collections.Generic;

namespace NextLoad.Models
{
	public class Connection
	{
		private static readonly NextLoadEntities db = new NextLoadEntities();

		public static Empresa GetConnByID(int id)
		{
			return db.Empresa.AsNoTracking().Single(c => c.ID == id);
		}

		public static List<Empresa> GetAllConns()
		{
			return db.Empresa.AsNoTracking().ToList(); ;
		}
	}
}