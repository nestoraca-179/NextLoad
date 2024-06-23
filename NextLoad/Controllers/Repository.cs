using NextLoad.Models;

namespace NextLoad.Controllers
{
    public abstract class Repository
    {
        public readonly static NextLoadEntities db = new NextLoadEntities();
    }
}